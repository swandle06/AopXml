using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    static class AopExtensions
    {
        public static IReadOnlyCollection<dataAop> MapToSchema(
            this IQueryable<Aop> aops,
            IReadOnlyDictionary<int, statusWikistatus> wikiStatusesByWikiId,
            IReadOnlyDictionary<int, statusOecdstatus> oecdStatusesByWikiId,
            IReadOnlyDictionary<int, statusSaaopstatus> saaopStatusesByWikiId,
            IQueryable<AopEvent> aopEvents,
            IReadOnlyDictionary<int, dataKeyevent> keyEventsByWikiId,
            IReadOnlyDictionary<int, dataKeyeventrelationship> keyEventRelationshipsByWikiId,
            IReadOnlyDictionary<int, confidenceleveltype> confidenceLevelsByWikiId,
            IReadOnlyDictionary<int, dataStressor> stressorsByWikiId,
            IReadOnlyDictionary<int, applicabilitytypeSexSex> sexesByWikiId,
            IReadOnlyDictionary<int, applicabilitytypeLifestageLifestage> lifeStagesByWikiId,
            TaxonomyMapper taxonomies)
        {
            if (aops == null) throw new ArgumentNullException(nameof(aops));
            if (wikiStatusesByWikiId == null) throw new ArgumentNullException(nameof(wikiStatusesByWikiId));
            if (oecdStatusesByWikiId == null) throw new ArgumentNullException(nameof(oecdStatusesByWikiId));
            if (saaopStatusesByWikiId == null) throw new ArgumentNullException(nameof(saaopStatusesByWikiId));
            if (aopEvents == null) throw new ArgumentNullException(nameof(aopEvents));
            if (keyEventsByWikiId == null) throw new ArgumentNullException(nameof(keyEventsByWikiId));
            if (keyEventRelationshipsByWikiId == null)
                throw new ArgumentNullException(nameof(keyEventRelationshipsByWikiId));
            if (confidenceLevelsByWikiId == null) throw new ArgumentNullException(nameof(confidenceLevelsByWikiId));
            if (stressorsByWikiId == null) throw new ArgumentNullException(nameof(stressorsByWikiId));
            if (sexesByWikiId == null) throw new ArgumentNullException(nameof(sexesByWikiId));
            if (lifeStagesByWikiId == null) throw new ArgumentNullException(nameof(lifeStagesByWikiId));
            if (taxonomies == null) throw new ArgumentNullException(nameof(taxonomies));

            return aops.Select(
                    x => new dataAop
                    {
                        id = Guid.NewGuid().ToString(),
                        title = x.Title,
                        shortname = x.ShortName,
                        @abstract = x.Abstract,
                        authors = x.Authors,
                        overallassessment = new dataAopOverallassessment
                        {
                            description = x.OverallAssessment,
                            applicability = x.ApplicabilityOfTheAop,
                            keyeventessentialitysummary = x.KeyEventEssentiality,
                            weightofevidencesummary = x.WeightOfEvidenceSummary,
                            quantitativeconsiderations = x.QuantitativeConsiderations
                        },
                        background = x.Background,
                        potentialapplications = x.OptionalConsiderations,
                        references = x.References,

                        status = new status
                        {
                            wikistatus = x.StatusId.HasValue
                                ? wikiStatusesByWikiId[x.StatusId.Value]
                                : statusWikistatus.UnderdevelopmentNotopenforcommentDonotcite,

                            oecdstatus = x.OecdStatusId.HasValue
                                ? oecdStatusesByWikiId[x.OecdStatusId.Value]
                                : statusOecdstatus.UnderDevelopment,

                            saaopstatus = x.SaaopStatusId.HasValue
                                ? saaopStatusesByWikiId[x.SaaopStatusId.Value]
                                : statusSaaopstatus.UnderDevelopment
                        },

                        applicability = new applicabilitytype
                        {
                            sex = x.AopSexes
                                .Where(s => s.SexTermId.HasValue)
                                .Select(
                                    s => new applicabilitytypeSex
                                    {
                                        sex = sexesByWikiId[s.SexTermId.Value],
                                        evidence = s.EvidenceId.HasValue
                                            ? confidenceLevelsByWikiId[s.EvidenceId.Value]
                                            : confidenceleveltype.notspecified
                                    })
                                .ToArray(),

                            lifestage = x.AopLifeStages
                                .Where(l => l.LifeStageTermId.HasValue)
                                .Select(
                                    l => new applicabilitytypeLifestage
                                    {
                                        lifestage = lifeStagesByWikiId[l.LifeStageTermId.Value],
                                        evidence = l.EvidenceId.HasValue
                                            ? confidenceLevelsByWikiId[l.EvidenceId.Value]
                                            : confidenceleveltype.notspecified
                                    })
                                .ToArray(),

                            taxonomy = x.AopTaxons
                                .Where(t => t.TaxonTermId.HasValue)
                                .Select(
                                    t => new applicabilitytypeTaxonomy
                                    {
                                        taxonomyid =
                                            taxonomies.GetByAopWikiId(t.TaxonTermId.Value).id,
                                        evidence = t.EvidenceId.HasValue
                                            ? confidenceLevelsByWikiId[t.EvidenceId.Value]
                                            : confidenceleveltype.notspecified
                                    })
                                .ToArray()
                        },

                        molecularinitiatingevent = aopEvents
                            .Where(
                                e => e.AopId == x.Id
                                     && e.Type == RelationshipType.MolecularInitiatingEvent.ToString()
                                     && e.EventId.HasValue)
                            .Select(
                                e => new dataAopMolecularinitiatingevent
                                {
                                    keyeventid = keyEventsByWikiId[e.EventId.Value].id,
                                    evidencesupportingchemicalinitiation = keyEventsByWikiId[e.EventId.Value]
                                        .evidencesupportingchemicalinitiation
                                }.SetWikiId(e.EventId.Value))
                            .ToArray(),

                        adverseoutcome = aopEvents
                            .Where(
                                e => e.AopId == x.Id
                                     && e.Type == RelationshipType.AdverseOutcome.ToString()
                                     && e.EventId.HasValue)
                            .Select(
                                e => new dataAopAdverseoutcome
                                {
                                    keyeventid = keyEventsByWikiId[e.EventId.Value].id,
                                    examples = keyEventsByWikiId[e.EventId.Value].examplesusingadverseoutcome
                                }.SetWikiId(e.EventId.Value))
                            .ToArray(),

                        keyeventrelationships = x.AopRelationships
                            .Where(r => r.RelationshipId.HasValue && r.Directness.HasValue)
                            .OrderBy(r => r.RowOrder)
                            .Select(
                                r => new dataAopRelationship
                                {
                                    id = keyEventRelationshipsByWikiId[r.RelationshipId.Value].id,
                                    directness = r.Directness.Value == 0 // inverted in the sql
                                        ? dataAopRelationshipDirectness.direct
                                        : dataAopRelationshipDirectness.indirect,
                                    evidence = r.EvidenceId.HasValue
                                        ? confidenceLevelsByWikiId[r.EvidenceId.Value]
                                        : confidenceleveltype.notspecified,
                                    quantitativeunderstandingvalue =
                                        r.QuantitativeUnderstandingId.HasValue
                                            ? confidenceLevelsByWikiId[r.QuantitativeUnderstandingId.Value]
                                            : confidenceleveltype.notspecified
                                }.SetWikiId(r.RelationshipId.Value))
                            .ToArray(),

                        keyeventessentialities = aopEvents
                            .Where(e => e.AopId == x.Id && e.EventId.HasValue)
                            .Select(
                                s => new dataAopEssentiality
                                {
                                    keyeventid = keyEventsByWikiId[s.EventId.Value].id,
                                    essentialitylevel = s.EssentialityId.HasValue
                                        ? confidenceLevelsByWikiId[s.EssentialityId.Value]
                                        : confidenceleveltype.notspecified,
                                }.SetWikiId(s.EventId.Value)).ToArray(),

                        aopstressors = x.AopStressors
                            .Where(s => s.StressorId.HasValue)
                            .Select(
                                s => new dataAopAopstressor
                                {
                                    id = stressorsByWikiId[s.StressorId.Value].id,
                                    evidence = s.EvidenceId.HasValue
                                        ? confidenceLevelsByWikiId[s.EvidenceId.Value]
                                        : confidenceleveltype.notspecified
                                }.SetWikiId(s.StressorId.Value))
                            .ToArray(),

                        creationtimestamp = x.CreatedAt,
                        lastmodificationtimestamp = x.UpdatedAt
                    }.SetWikiId(x.Id))
                .ToList();
        }

        enum RelationshipType
        {
            // KeyEvent = 0,
            MolecularInitiatingEvent = 1,
            AdverseOutcome = 2
        }
    }
}