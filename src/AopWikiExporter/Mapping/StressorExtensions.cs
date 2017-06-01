using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    static class StressorExtensions
    {
        public static IReadOnlyCollection<IWikiReference<dataStressor>> MapToSchema(
            this IQueryable<Stressor> stressors,
            IReadOnlyDictionary<int, IWikiReference<dataChemical>> chemicalsByWikiId)
        {
            if (stressors == null) throw new ArgumentNullException(nameof(stressors));
            if (chemicalsByWikiId == null) throw new ArgumentNullException(nameof(chemicalsByWikiId));

            return stressors.Select(
                    x => new
                    {
                        AopWikiId = x.Id,
                        MappedTarget = new dataStressor
                        {
                            id = Guid.NewGuid().ToString(),
                            name = x.Name,
                            description = x.ChemicalDescription,
                            exposurecharacterization = x.CharacterizationOfExposure,
                            creationtimestamp = x.CreatedAt,
                            lastmodificationtimestamp = x.UpdatedAt
                        },
                        Chemicals = x.StressorChemicals.Select(
                            c => new
                            {
                                AopWikiId = c.ChemicalId,
                                UserTerm = c.UserTerm != null ? c.UserTerm.Trim() : null
                            })
                    })
                .ToList() // Execute query
                .Select(
                    x =>
                    {
                        var reference = new WikiReference<dataStressor>
                        {
                            AopWikiId = x.AopWikiId,
                            Target = x.MappedTarget
                        };
                        reference.Target.chemicals = x.Chemicals.Select(
                                c => new dataStressorChemicalinitiator
                                {
                                    chemicalid = c.AopWikiId.HasValue
                                        ? chemicalsByWikiId[c.AopWikiId.Value].Target.id
                                        : null,
                                    userterm = c.UserTerm
                                })
                            .ToArray();

                        return reference;
                    })
                .ToList();
        }
    }
}