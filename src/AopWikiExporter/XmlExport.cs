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
        readonly int? _targetId;
        readonly TargetType? _targetType;

        public XmlExport(
            string connectionString, 
            bool excludeUnreferencedElements, 
            int? targetId, 
            TargetType? targetType)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(connectionString));

            this._connectionString = connectionString;
            this._excludeUnreferencedElements = targetId.HasValue || excludeUnreferencedElements;
            this._targetId = targetId;
            this._targetType = targetType;
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
                var enumsByWikiId = EnumsByWikiId.CreateFrom(context);

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
                var uniqueTaxonomiesByXmlId = taxonomyMapper.GetUniqueMappedObjectsByXmlId();
                var uniqueTaxonomies = uniqueTaxonomiesByXmlId.Values;

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

                var biologicalObjectsByWikiId = context.BiologicalObjects
                    .Select(x => x.MapToSchema(enumsByWikiId.BiologicalOrganizations))
                    .ToList()
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var keyEventsByWikiId = context
                    .Events
                    .MapToSchema(
                        organTermsByWikiId,
                        cellTermsByWikiId,
                        biologicalObjectsByWikiId,
                        biologicalProcessesByWikiId,
                        biologicalActionsByWikiId,
                        enumsByWikiId.ConfidenceLevels,
                        enumsByWikiId.Sexes,
                        enumsByWikiId.LifeStages,
                        taxonomyMapper,
                        stressorsByWikiId)
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var keyEventRelationshipsByWikiId = context
                    .Relationships
                    .MapToSchema(keyEventsByWikiId)
                    .ToDictionary(x => x.GetWikiId(), x => x);

                var aopsByWikiId = context
                    .Aops
                    .MapToSchema(
                        context.AopEvents,
                        keyEventsByWikiId,
                        keyEventRelationshipsByWikiId,
                        stressorsByWikiId,
                        taxonomyMapper,
                        enumsByWikiId)
                    .ToDictionary(x => x.GetWikiId(), x => x);

                IReadOnlyCollection<dataAop> targetAops = aopsByWikiId.Values;
                if (this._excludeUnreferencedElements)
                {
                    if (this._targetType == TargetType.Aop
                        && this._targetId.HasValue
                        && aopsByWikiId.TryGetValue(this._targetId.Value, out var matchedAop))
                    {
                        targetAops = new[] { matchedAop };
                    }
                    else if (this._targetType == TargetType.KeyEvent)
                    {
                        targetAops = new dataAop[0];
                    }

                    keyEventRelationshipsByWikiId = targetAops.SelectMany(x => x.keyeventrelationships)
                        .Where(x => x != null)
                        .GroupBy(x => x.id)
                        .Select(x => x.First())
                        .ToDictionary(x => x.GetWikiId(), x => keyEventRelationshipsByWikiId[x.GetWikiId()]);

                    keyEventsByWikiId = this._targetType == TargetType.KeyEvent
                                        && this._targetId.HasValue
                                        && keyEventsByWikiId.TryGetValue(this._targetId.Value, out var matchedKeyEvent)
                        ? new Dictionary<int, dataKeyevent> { [this._targetId.Value] = matchedKeyEvent }
                        : targetAops
                            .SelectMany(
                                x => (x.molecularinitiatingevent?.Select(m => m?.GetWikiId() ?? 0) ?? new int[0])
                                    .Union(x.adverseoutcome?.Select(a => a?.GetWikiId() ?? 0) ?? new int[0])
                                    .Union(x.keyeventessentialities?.Select(e => e?.GetWikiId() ?? 0) ?? new int[0]))
                            .Union(
                                keyEventRelationshipsByWikiId
                                    .Values
                                    .SelectMany(
                                        k => new[]
                                        {
                                            k.title?.upstreamid?.GetWikiId(), k.title?.downstreamid?.GetWikiId()
                                        })
                                    .Where(v => v != null).Select(v => v.Value))
                            .Distinct()
                            .ToDictionary(x => x, x => keyEventsByWikiId[x]);

                    uniqueTaxonomies = targetAops
                        .SelectMany(x => x.applicability?.taxonomy.Select(t => t?.taxonomyid))
                        .Union(
                            keyEventsByWikiId.Values.SelectMany(
                                x => x.applicability?.taxonomy.Select(t => t?.taxonomyid)))
                        .Distinct()
                        .Select(x => uniqueTaxonomiesByXmlId.TryGetValue(x, out var taxonomy) ? taxonomy : null)
                        .Where(x => x != null)
                        .ToList();

                    biologicalActionsByWikiId = keyEventsByWikiId.Values
                        .SelectMany(x => x.biologicalevents?.Select(e => e?.actionid?.GetWikiId()))
                        .Where(x => x.HasValue)
                        .Select(x => x.Value)
                        .Distinct()
                        .ToDictionary(x => x, x => biologicalActionsByWikiId[x]);

                    biologicalProcessesByWikiId = keyEventsByWikiId.Values
                        .SelectMany(x => x.biologicalevents?.Select(e => e?.processid?.GetWikiId()))
                        .Where(x => x.HasValue)
                        .Select(x => x.Value)
                        .Distinct()
                        .ToDictionary(x => x, x => biologicalProcessesByWikiId[x]);

                    biologicalObjectsByWikiId = keyEventsByWikiId.Values
                        .SelectMany(x => x.biologicalevents?.Select(e => e?.objectid?.GetWikiId()))
                        .Where(x => x.HasValue)
                        .Select(x => x.Value)
                        .Distinct()
                        .ToDictionary(x => x, x => biologicalObjectsByWikiId[x]);

                    stressorsByWikiId = targetAops
                        .SelectMany(x => x.aopstressors?.Select(s => s.GetWikiId()) ?? new int[0])
                        .Union(keyEventsByWikiId.Values.SelectMany(k => k.keyeventstressors?.Select(s => s.GetWikiId())))
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
                uniqueTaxonomies.AddToReferencesAndAssignToRoot(referenceLists, data);
                chemicalsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                stressorsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                biologicalObjectsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                keyEventsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                keyEventRelationshipsByWikiId.Values.AddToReferencesAndAssignToRoot(referenceLists, data);
                targetAops.AddToReferencesAndAssignToRoot(referenceLists, data);

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
