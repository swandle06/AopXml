using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    static class StressorExtensions
    {
        public static IReadOnlyCollection<dataStressor> MapToSchema(
            this IQueryable<Stressor> stressors,
            IReadOnlyDictionary<int, dataChemical> chemicalsByWikiId)
        {
            if (stressors == null) throw new ArgumentNullException(nameof(stressors));
            if (chemicalsByWikiId == null) throw new ArgumentNullException(nameof(chemicalsByWikiId));

            return stressors.Select(
                    x => new
                    {
                        MappedTarget = new dataStressor
                        {
                            id = Guid.NewGuid().ToString(),
                            name = x.Name,
                            description = x.ChemicalDescription,
                            exposurecharacterization = x.CharacterizationOfExposure,
                            creationtimestamp = x.CreatedAt,
                            creationtimestampSpecified = x.CreatedAt != default(DateTime),
                            lastmodificationtimestamp = x.UpdatedAt,
                            lastmodificationtimestampSpecified = x.UpdatedAt != default(DateTime)
                        }.SetWikiId(x.Id),
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
                        x.MappedTarget.chemicals = x.Chemicals.Select(
                                c => new dataStressorChemicalinitiator
                                {
                                    chemicalid = c.AopWikiId.HasValue
                                        ? chemicalsByWikiId[c.AopWikiId.Value].id
                                        : null,
                                    userterm = c.UserTerm?.Trim()
                                }.SetWikiId(c.AopWikiId.Value))
                            .ToArray();
                        return x.MappedTarget;
                    })
                .ToList();
        }
    }
}