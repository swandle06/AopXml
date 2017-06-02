using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    static class RelationshipExtensions
    {
        public static IReadOnlyCollection<dataKeyeventrelationship> MapToSchema(
            this IQueryable<Relationship> relationships,
            IReadOnlyDictionary<int, dataKeyevent> keyEventsByWikiId)
        {
            if (relationships == null) throw new ArgumentNullException(nameof(relationships));
            if (keyEventsByWikiId == null) throw new ArgumentNullException(nameof(keyEventsByWikiId));

            return relationships.Select(
                x => new dataKeyeventrelationship
                {
                    id = Guid.NewGuid().ToString(),
                    title = new dataKeyeventrelationshipTitle
                    {
                        upstreamid = x.UpstreamEventId.HasValue
                            ? keyEventsByWikiId[x.UpstreamEventId.Value].id.SetWikiId(x.UpstreamEventId.Value)
                            : null,
                        downstreamid = x.DownstreamEventId.HasValue
                            ? keyEventsByWikiId[x.DownstreamEventId.Value].id.SetWikiId(x.DownstreamEventId.Value)
                            : null
                    },
                    description = x.HowItWorks,
                    weightofevidence = new dataKeyeventrelationshipWeightofevidence
                    {
                        biologicalplausibility = x.BiologicalPlausibility,
                        empericalsupportlinkage = x.EmpiricalSupport,
                        uncertaintiesorinconsistencies = x.Uncertainties,
                        value = x.WeightOfEvidence
                    },
                    quantitativeunderstanding = new dataKeyeventrelationshipQuantitativeunderstanding
                    {
                        description = x.QuantitativeUnderstanding
                    },
                    evidencesupportingtaxonomicapplicability = x.TaxonEvidence,
                    references = x.References,
                    creationtimestamp = x.CreatedAt,
                    lastmodificationtimestamp = x.UpdatedAt
                }.SetWikiId(x.Id)
            ).ToList();
        }
    }
}