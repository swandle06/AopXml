using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    static class ChemicalExtensions
    {
        public static IReadOnlyCollection<dataChemical> MapToSchema(
            this IQueryable<Chemical> chemicals,
            IQueryable<ChemicalSynonym> chemicalSynonyms)
        {
            if (chemicals == null) throw new ArgumentNullException(nameof(chemicals));
            if (chemicalSynonyms == null) throw new ArgumentNullException(nameof(chemicalSynonyms));

            var mappedChemicals = chemicals.Select(
                    x => new dataChemical
                    {
                        id = Guid.NewGuid().ToString(),
                        casrn = x.Casrn != null ? x.Casrn.Trim() : null,
                        dsstoxid = x.DsstoxSubstanceId != null ? x.DsstoxSubstanceId.Trim() : null,
                        jcheminchikey = x.JchemInchiKey != null ? x.JchemInchiKey.Trim() : null,
                        indigoinchikey = x.IndigoInchiKey != null ? x.IndigoInchiKey.Trim() : null,
                        preferredname = x.PreferredName != null ? x.PreferredName.Trim() : null
                    }.SetWikiId(x.Id)
                )
                .ToList();

            var synonymJoinTable = chemicalSynonyms
                .Where(x => x.ChemicalId.HasValue && chemicals.Any(c => c.Id == x.ChemicalId.Value))
                .GroupBy(x => x.ChemicalId)
                .ToDictionary(x => x.Key, x => x);

            foreach (var mappedChemical in mappedChemicals)
            {
                if (synonymJoinTable.TryGetValue(mappedChemical.GetWikiId(), out var synonyms))
                {
                    mappedChemical.synonyms = synonyms.Where(t => t.Term != null)
                        .Select(t => t.Term.Trim())
                        .ToArray();
                }
            }

            return mappedChemicals;
        }
    }
}