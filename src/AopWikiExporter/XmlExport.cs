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
        readonly bool _excludeUnreferencedChemicals;

        public XmlExport(string connectionString, bool excludeUnreferencedChemicals)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(connectionString));

            this._connectionString = connectionString;
            this._excludeUnreferencedChemicals = excludeUnreferencedChemicals;
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
                var referenceLists = new List<IEnumerable<IWikiReference<IXmlIdentifiable>>>();

                var biologicalProcessesByWikiId = context.BiologicalProcesses
                    .Select(x => x.MapToSchema())
                    .ToList()
                    .AddToReferencesAndAssignToRoot(referenceLists, data)
                    .ToDictionary(x => x.AopWikiId, x => x);

                var biologicalActionsByWikiId = context.BiologicalActions
                    .Select(x => x.MapToSchema())
                    .ToList()
                    .AddToReferencesAndAssignToRoot(referenceLists, data)
                    .ToDictionary(x => x.AopWikiId, x => x);

                var taxonomyMapper = new TaxonomyMapper(context.TaxonTerms);
                taxonomyMapper.GetUniqueMappedObjects().AddToReferencesAndAssignToRoot(referenceLists, data);

                var chemicalsByWikiId = context.Chemicals
                    .MapToSchema(context.ChemicalSynonyms)
                    .AddToReferencesAndAssignToRoot(referenceLists, data)
                    .ToDictionary(x => x.AopWikiId, x => x);

                var stressorsByWikiId = context.Stressors
                    .MapToSchema(chemicalsByWikiId)
                    .AddToReferencesAndAssignToRoot(referenceLists, data)
                    .ToDictionary(x => x.AopWikiId, x => x);

                var cellTermsByWikiId = context.CellTerms
                    .ToList()
                    .Select(x => x.MapToSchema())
                    .ToDictionary(x => x.AopWikiId, x => x);

                var organTermsByWikiId = context.OrganTerms
                    .ToList()
                    .Select(x => x.MapToSchema())
                    .ToDictionary(x => x.AopWikiId, x => x);

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
                    .AddToReferencesAndAssignToRoot(referenceLists, data)
                    .ToDictionary(x => x.AopWikiId, x => x);

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
                    .AddToReferencesAndAssignToRoot(referenceLists, data)
                    .ToDictionary(x => x.AopWikiId, x => x);

                var keyEventRelationshipsByWikiId = context
                    .Relationships
                    .MapToSchema(keyEventByWikiId)
                    .AddToReferencesAndAssignToRoot(referenceLists, data)
                    .ToDictionary(x => x.AopWikiId, x => x);

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
                        taxonomyMapper)
                    .AddToReferencesAndAssignToRoot(referenceLists, data);

                if (this._excludeUnreferencedChemicals)
                {
                    // Slow but does the job for now
                    var referencedStressorIds = new HashSet<string>(
                        aops.SelectMany(a => a.Target.aopstressors.Select(y => y.id)));

                    var referencedChemicalIds = new HashSet<string>(
                        stressorsByWikiId
                            .Select(s => s.Value.Target)
                            .Where(s => referencedStressorIds.Contains(s.id))
                            .SelectMany(y => y.chemicals.Select(c => c.chemicalid)));

                    var referencedChemicals =
                        chemicalsByWikiId.Where(x => referencedChemicalIds.Contains(x.Value.Target.id));

                    referenceLists = referenceLists
                        .Where(x => !(x.FirstOrDefault() is IWikiReference<dataChemical>))
                        .ToList();

                    referencedChemicals.Select(x => x.Value)
                        .ToList()
                        .AddToReferencesAndAssignToRoot(referenceLists, data);
                }

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
                        typeof(data).GetTypeInfo().GetCustomAttribute<XmlTypeAttribute>()?.Namespace ?? string.Empty);
                    var rootSerializer = new XmlSerializer(data.GetType());
                    rootSerializer.Serialize(writer, data, namespaces);
                }

                tx.Commit();
            }
        }
    }
}