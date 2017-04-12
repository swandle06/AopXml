using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    static class KeyEventExtensions
    {
        public static IReadOnlyCollection<IWikiReference<dataKeyevent>> MapToSchema(
            this IQueryable<Event> events,
            IReadOnlyDictionary<int, IWikiReference<biologicaltermtype>> organTermsByWikiId,
            IReadOnlyDictionary<int, IWikiReference<biologicaltermtype>> cellTermsByWikiId,
            IReadOnlyDictionary<int, IWikiReference<dataBiologicalobject>> biologicalObjectsByWikiId,
            IReadOnlyDictionary<int, IWikiReference<dataBiologicalprocess>> biologicalProcessesByWikiId,
            IReadOnlyDictionary<int, IWikiReference<dataBiologicalaction>> biologicalActionsByWikiId,
            IReadOnlyDictionary<int, confidenceleveltype> confidenceLevelsByWikiId,
            IReadOnlyDictionary<int, applicabilitytypeSexSex> sexesByWikiId,
            IReadOnlyDictionary<int, applicabilitytypeLifestageLifestage> lifeStagesByWikiId,
            TaxonomyMapper taxonomies,
            IReadOnlyDictionary<int, IWikiReference<dataStressor>> stressorsByWikiId)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));
            if (organTermsByWikiId == null) throw new ArgumentNullException(nameof(organTermsByWikiId));
            if (cellTermsByWikiId == null) throw new ArgumentNullException(nameof(cellTermsByWikiId));
            if (biologicalObjectsByWikiId == null) throw new ArgumentNullException(nameof(biologicalObjectsByWikiId));
            if (biologicalProcessesByWikiId == null)
                throw new ArgumentNullException(nameof(biologicalProcessesByWikiId));
            if (biologicalActionsByWikiId == null) throw new ArgumentNullException(nameof(biologicalActionsByWikiId));
            if (confidenceLevelsByWikiId == null) throw new ArgumentNullException(nameof(confidenceLevelsByWikiId));
            if (sexesByWikiId == null) throw new ArgumentNullException(nameof(sexesByWikiId));
            if (lifeStagesByWikiId == null) throw new ArgumentNullException(nameof(lifeStagesByWikiId));
            if (taxonomies == null) throw new ArgumentNullException(nameof(taxonomies));
            if (stressorsByWikiId == null) throw new ArgumentNullException(nameof(stressorsByWikiId));

            return events.Select(
                    x => new WikiReference<dataKeyevent>
                    {
                        AopWikiId = x.Id,
                        Target = new dataKeyevent
                        {
                            id = Guid.NewGuid().ToString(),
                            title = x.Title,
                            shortname = x.ShortName,
                            description = x.HowItWorks,
                            measurementmethodology = x.MeasuredOrDetected,
                            evidencesupportingtaxonomicapplicability = x.SupportingTaxEvidence,
                            references = x.References,

                            organterm = x.OrganTermId.HasValue
                                ? organTermsByWikiId[x.OrganTermId.Value].Target
                                : null,
                            cellterm = x.CellTermId.HasValue
                                ? cellTermsByWikiId[x.CellTermId.Value].Target
                                : null,

                            biologicalevents = x.EventSubEvents.Select(
                                    s => new dataKeyeventBiologicalevent
                                    {
                                        objectid = s.SubEvent.BiologicalObjectId.HasValue
                                            ? biologicalObjectsByWikiId[s.SubEvent.BiologicalObjectId.Value]
                                                .Target.id
                                            : null,
                                        processid = s.SubEvent.BiologicalProcessId.HasValue
                                            ? biologicalProcessesByWikiId[s.SubEvent.BiologicalProcessId.Value]
                                                .Target.id
                                            : null,
                                        actionid = s.SubEvent.BiologicalActionId.HasValue
                                            ? biologicalActionsByWikiId[s.SubEvent.BiologicalActionId.Value]
                                                .Target.id
                                            : null
                                    })
                                .ToArray(),

                            applicability = new applicabilitytype
                            {
                                sex = x.EventSexes
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

                                lifestage = x.EventLifeStages
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

                                taxonomy = x.EventTaxons
                                    .Where(t => t.TaxonTermId.HasValue)
                                    .Select(
                                        t => new applicabilitytypeTaxonomy
                                        {
                                            taxonomyid =
                                                taxonomies.GetByAopWikiId(t.TaxonTermId.Value).Target.id,
                                            evidence = t.EvidenceId.HasValue
                                                ? confidenceLevelsByWikiId[t.EvidenceId.Value]
                                                : confidenceleveltype.notspecified
                                        })
                                    .ToArray()
                            },

                            keyeventstressors = x.EventStressors
                                .Where(s => s.StressorId.HasValue)
                                .Select(
                                    s => new dataKeyeventKeyeventstressor
                                    {
                                        id = stressorsByWikiId[s.StressorId.Value].Target.id,
                                        //evidence = s.EvidenceId.HasValue
                                        //    ? confidenceLevelsByWikiId[s.EvidenceId.Value]
                                        //    : confidenceleveltype.notspecified,
                                        description = s.EvidenceText
                                    })
                                .ToArray(),

                            creationtimestamp = x.CreatedAt,
                            lastmodificationtimestamp = x.UpdatedAt,

                            evidencesupportingchemicalinitiation =
                                x.EvidenceForChemicalInitiation, // Extension for mie
                            examplesusingadverseoutcome = x.ExamplesUsingAo // Extension for ao
                        }
                    })
                .ToList();
        }
    }
}