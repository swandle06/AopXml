using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AopWikiExporter.Data;
using AopWikiExporter.Mapping;

namespace AopWikiExporter
{
    class XmlExport
    {
        readonly string _connectionString;
        readonly bool _excludeUnreferencedElements;

        public XmlExport(string connectionString, bool excludeUnreferencedElements)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(connectionString));

            this._connectionString = connectionString;
            this._excludeUnreferencedElements = excludeUnreferencedElements;
        }

        public void WriteToOutput(Stream output)
        {
            if (output == null) throw new ArgumentNullException(nameof(output));

            using (var context = new AopWikiDbContext(this._connectionString)
            {
                ChangeTracker = { AutoDetectChangesEnabled = false }
            })
            using (var tx = context.Database.BeginTransaction())
            {
                var data = new data();

                var biologicalProcessesByWikiId = context.BiologicalProcesses
                    .Select(x => x.MapToSchema())
                    .ToList()
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var biologicalActionsByWikiId = context.BiologicalActions
                    .Select(x => x.MapToSchema())
                    .ToList()
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var taxonomyMapper = new TaxonomyMapper(context.TaxonTerms);

                var chemicalsByWikiId = context.Chemicals
                    .MapToSchema(context.ChemicalSynonyms)
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var stressorsByWikiId = context.Stressors
                    .MapToSchema(chemicalsByWikiId)
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var cellTermsByWikiId = context.CellTerms
                    .ToList()
                    .Select(x => x.MapToSchema())
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var organTermsByWikiId = context.OrganTerms
                    .ToList()
                    .Select(x => x.MapToSchema())
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var lifeStagesByWikiId = context
                    .LifeStageTerms
                    .MapToLookupTable<LifeStageTerm, applicabilitytypeLifestageLifestage>();

                var sexesByWikiId = context
                    .SexTerms
                    .MapToLookupTable<SexTerm, applicabilitytypeSexSex>();

                var biologicalOrganizationsByWikiId = context
                    .BiologicalOrganizations
                    .MapToLookupTable<BiologicalOrganization, biologicalorganizationleveltype>();

                var biologicalObjectsByWikiId = context.BiologicalObjects
                    .Select(x => x.MapToSchema(biologicalOrganizationsByWikiId))
                    .ToList()
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var confidenceLevelsByWikiId = context
                    .ConfidenceLevels
                    .MapToLookupTable<ConfidenceLevel, confidenceleveltype>();

                var keyEventByWikiId = context
                    .Events
                    .MapToSchema(
                        organTermsByWikiId,
                        cellTermsByWikiId,
                        biologicalObjectsByWikiId,
                        biologicalProcessesByWikiId,
                        biologicalActionsByWikiId,
                        confidenceLevelsByWikiId,
                        sexesByWikiId,
                        lifeStagesByWikiId,
                        taxonomyMapper,
                        stressorsByWikiId)
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var keyEventRelationshipsByWikiId = context
                    .Relationships
                    .MapToSchema(keyEventByWikiId)
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var wikiStatusesByWikiId = context
                    .Statuses
                    .MapToLookupTable(
                        new Dictionary<string, statusWikistatus>
                        {
                            ["Open for adopton"] = statusWikistatus.Openforadoption
                        });

                var oecdStatusesByWikiId = context
                    .OecdStatuses
                    .MapToLookupTable<OecdStatus, statusOecdstatus>();

                var saaopStatusesByWikiId = context
                    .SaaopStatuses
                    .MapToLookupTable<SaaopStatus, statusSaaopstatus>();

                var aops = context
                    .Aops
                    .MapToSchema(
                        wikiStatusesByWikiId,
                        oecdStatusesByWikiId,
                        saaopStatusesByWikiId,
                        context.AopEvents,
                        keyEventByWikiId,
                        keyEventRelationshipsByWikiId,
                        confidenceLevelsByWikiId,
                        stressorsByWikiId,
                        sexesByWikiId,
                        lifeStagesByWikiId,
                        taxonomyMapper);

                if (this._excludeUnreferencedElements)
                {
                    keyEventRelationshipsByWikiId = aops.SelectMany(x => x.keyeventrelationships)
                        .Where(x => x != null)
                        .GroupBy(x => x.id)
                        .Select(x => x.First())
                        .ToDictionary(x => x.GetWikiId(), x => keyEventRelationshipsByWikiId[x.GetWikiId()]);

                    keyEventByWikiId = aops
                        .SelectMany(
                            x => (x.molecularinitiatingevent?.Select(m => m?.GetWikiId() ?? 0) ?? new int[0])
                                .Union(x.adverseoutcome?.Select(a => a?.GetWikiId() ?? 0) ?? new int[0])
                                .Union(x.keyeventessentialities?.Select(e => e?.GetWikiId() ?? 0) ?? new int[0]))
                        .Union(
                            keyEventRelationshipsByWikiId
                                .Values
                                .SelectMany(
                                    k => new[] { k.title?.upstreamid?.GetWikiId(), k.title?.downstreamid?.GetWikiId() })
                                .Where(v => v != null).Select(v => v.Value))
                        .Distinct()
                        .ToDictionary(x => x, x => keyEventByWikiId[x]);

                    stressorsByWikiId = aops
                        .SelectMany(x => x.aopstressors?.Select(s => s.GetWikiId()) ?? new int[0])
                        .Union(keyEventByWikiId.Values.SelectMany(k => k.keyeventstressors?.Select(s => s.GetWikiId())))
                        .Distinct()
                        .ToDictionary(x => x, x => stressorsByWikiId[x]);

                    chemicalsByWikiId = stressorsByWikiId
                        .Values
                        .SelectMany(x => x.chemicals?.Select(c => c.GetWikiId()) ?? new int[0])
                        .Distinct()
                        .ToDictionary(x => x, x => chemicalsByWikiId[x]);
                }

                var referenceLists = new List<IEnumerable<IXmlIdentifiable>>();
                biologicalProcessesByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                biologicalActionsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                taxonomyMapper.GetUniqueMappedObjects().AddToReferencesAndAssignToRoot(referenceLists, data);
                chemicalsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                stressorsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                biologicalObjectsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                keyEventByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                keyEventRelationshipsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                aops.AddToReferencesAndAssignToRoot(referenceLists, data);

                data.vendorspecific = new dataVendorspecific
                {
                    id = Guid.NewGuid().ToString(),
                    name = "AopWiki",
                    version = DateTime.UtcNow.ToString("O"),
                    Any = referenceLists.SelectMany(x => x, (x, i) => i.ToXmlElement()).ToArray()
                };

                using (var writer = XmlWriter.Create(
                    output,
                    new XmlWriterSettings
                    {
                        Indent = true,
                        IndentChars = "    ",
                        ConformanceLevel = ConformanceLevel.Document,
                        WriteEndDocumentOnClose = true,
                        NamespaceHandling = NamespaceHandling.OmitDuplicates,
                        NewLineChars = "\n",
                        Encoding = Encoding.UTF8,
                        CloseOutput = false,
                        NewLineOnAttributes = false
                    }))
                {
                    // Gets rid of xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                    var namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(
                        string.Empty,
                        typeof(data).GetTypeInfo().GetCustomAttribute<XmlTypeAttribute>()?.Namespace
                        ?? string.Empty);
                    var rootSerializer = new XmlSerializer(data.GetType());
                    rootSerializer.Serialize(writer, data, namespaces);
                }

                tx.Commit();
            }
        }
    }
}
