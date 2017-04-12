using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    static class RelationshipExtensions
    {
        public static IReadOnlyCollection<IWikiReference<dataKeyeventrelationship>> MapToSchema(
            this IQueryable<Relationship> relationships,
            IReadOnlyDictionary<int, IWikiReference<dataKeyevent>> keyEventsByWikiId)
        {
            if (relationships == null) throw new ArgumentNullException(nameof(relationships));
            if (keyEventsByWikiId == null) throw new ArgumentNullException(nameof(keyEventsByWikiId));

            return relationships.Select(
                    x => new WikiReference<dataKeyeventrelationship>
                    {
                        AopWikiId = x.Id,
                        Target = new dataKeyeventrelationship
                        {
                            id = Guid.NewGuid().ToString(),
                            title = new dataKeyeventrelationshipTitle
                            {
                                upstreamid = x.UpstreamEventId.HasValue
                                    ? keyEventsByWikiId[x.UpstreamEventId.Value].Target.id
                                    : null,
                                downstreamid = x.DownstreamEventId.HasValue
                                    ? keyEventsByWikiId[x.DownstreamEventId.Value].Target.id
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
                        }
                    })
                .ToList();
        }
    }
}