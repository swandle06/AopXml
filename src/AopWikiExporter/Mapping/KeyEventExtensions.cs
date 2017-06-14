using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    static class KeyEventExtensions
    {
        public static IReadOnlyCollection<dataKeyevent> MapToSchema(
            this IQueryable<Event> events,
            IReadOnlyDictionary<int, biologicaltermtype> organTermsByWikiId,
            IReadOnlyDictionary<int, biologicaltermtype> cellTermsByWikiId,
            IReadOnlyDictionary<int, dataBiologicalobject> biologicalObjectsByWikiId,
            IReadOnlyDictionary<int, dataBiologicalprocess> biologicalProcessesByWikiId,
            IReadOnlyDictionary<int, dataBiologicalaction> biologicalActionsByWikiId,
            EnumsByWikiId enumsByWikiId,
            TaxonomyMapper taxonomies,
            IReadOnlyDictionary<int, dataStressor> stressorsByWikiId)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));
            if (organTermsByWikiId == null) throw new ArgumentNullException(nameof(organTermsByWikiId));
            if (cellTermsByWikiId == null) throw new ArgumentNullException(nameof(cellTermsByWikiId));
            if (biologicalObjectsByWikiId == null) throw new ArgumentNullException(nameof(biologicalObjectsByWikiId));
            if (biologicalProcessesByWikiId == null)
                throw new ArgumentNullException(nameof(biologicalProcessesByWikiId));
            if (biologicalActionsByWikiId == null) throw new ArgumentNullException(nameof(biologicalActionsByWikiId));
            if (enumsByWikiId == null) throw new ArgumentNullException(nameof(enumsByWikiId));
            if (taxonomies == null) throw new ArgumentNullException(nameof(taxonomies));
            if (stressorsByWikiId == null) throw new ArgumentNullException(nameof(stressorsByWikiId));

            return events.Select(
                    x => new dataKeyevent
                    {
                        id = Guid.NewGuid().ToString(),
                        title = x.Title,
                        shortname = x.ShortName,
                        description = x.HowItWorks,
                        measurementmethodology = x.MeasuredOrDetected,
                        evidencesupportingtaxonomicapplicability = x.SupportingTaxEvidence,
                        references = x.References,

                        organterm = x.OrganTermId.HasValue
                            ? organTermsByWikiId[x.OrganTermId.Value]
                            : null,
                        cellterm = x.CellTermId.HasValue
                            ? cellTermsByWikiId[x.CellTermId.Value]
                            : null,

                        biologicalevents = x.EventSubEvents.Select(
                                s => new dataKeyeventBiologicalevent
                                {
                                    objectid = s.SubEvent.BiologicalObjectId.HasValue
                                        ? biologicalObjectsByWikiId[s.SubEvent.BiologicalObjectId.Value].id
                                            .SetWikiId(s.SubEvent.BiologicalObjectId.Value)
                                        : null,
                                    processid = s.SubEvent.BiologicalProcessId.HasValue
                                        ? biologicalProcessesByWikiId[s.SubEvent.BiologicalProcessId.Value].id
                                            .SetWikiId(s.SubEvent.BiologicalProcessId.Value)
                                        : null,
                                    actionid = s.SubEvent.BiologicalActionId.HasValue
                                        ? biologicalActionsByWikiId[s.SubEvent.BiologicalActionId.Value].id
                                            .SetWikiId(s.SubEvent.BiologicalActionId.Value)
                                        : null
                                })
                            .ToArray(),

                        biologicalorganizationlevel = x.BiologicalOrganizationId.HasValue
                            ? enumsByWikiId.BiologicalOrganizations[x.BiologicalOrganizationId.Value]
                            : default(biologicalorganizationleveltype),

                        biologicalorganizationlevelSpecified = x.BiologicalOrganizationId.HasValue,

                        applicability = new applicabilitytype
                        {
                            sex = x.EventSexes
                                .Where(s => s.SexTermId.HasValue)
                                .Select(
                                    s => new applicabilitytypeSex
                                    {
                                        sex = enumsByWikiId.Sexes[s.SexTermId.Value],
                                        evidence = s.EvidenceId.HasValue
                                            ? enumsByWikiId.ConfidenceLevels[s.EvidenceId.Value]
                                            : confidenceleveltype.notspecified
                                    })
                                .ToArray(),

                            lifestage = x.EventLifeStages
                                .Where(l => l.LifeStageTermId.HasValue)
                                .Select(
                                    l => new applicabilitytypeLifestage
                                    {
                                        lifestage = enumsByWikiId.LifeStages[l.LifeStageTermId.Value],
                                        evidence = l.EvidenceId.HasValue
                                            ? enumsByWikiId.ConfidenceLevels[l.EvidenceId.Value]
                                            : confidenceleveltype.notspecified
                                    })
                                .ToArray(),

                            taxonomy = x.EventTaxons
                                .Where(t => t.TaxonTermId.HasValue)
                                .Select(
                                    t => new applicabilitytypeTaxonomy
                                    {
                                        taxonomyid =
                                            taxonomies.GetByAopWikiId(t.TaxonTermId.Value).id,
                                        evidence = t.EvidenceId.HasValue
                                            ? enumsByWikiId.ConfidenceLevels[t.EvidenceId.Value]
                                            : confidenceleveltype.notspecified
                                    })
                                .ToArray()
                        },

                        keyeventstressors = x.EventStressors
                            .Where(s => s.StressorId.HasValue)
                            .Select(
                                s => new dataKeyeventKeyeventstressor
                                {
                                    id = stressorsByWikiId[s.StressorId.Value].id
                                }.SetWikiId(s.StressorId.Value))
                            .ToArray(),

                        creationtimestamp = x.CreatedAt,
                        creationtimestampSpecified = x.CreatedAt != default(DateTime),
                        lastmodificationtimestamp = x.UpdatedAt,
                        lastmodificationtimestampSpecified = x.UpdatedAt != default(DateTime),

                        evidencesupportingchemicalinitiation = x.EvidenceForChemicalInitiation, // Extension for mie
                        examplesusingadverseoutcome = x.ExamplesUsingAo // Extension for ao
                    }.SetWikiId(x.Id)
                )
                .ToList();
        }
    }
}