using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    class TaxonomyMapper
    {
        readonly IDictionary<int, dataTaxonomy> _mappedTaxonomiesByAopWikiTermId;
        readonly Dictionary<string, dataTaxonomy> _uniqueTaxonomiesByXmlId;

        public TaxonomyMapper(IQueryable<TaxonTerm> allTaxonTerms)
        {
            if (allTaxonTerms == null) throw new ArgumentNullException(nameof(allTaxonTerms));

            this._mappedTaxonomiesByAopWikiTermId = new Dictionary<int, dataTaxonomy>();
            this._uniqueTaxonomiesByXmlId = new Dictionary<string, dataTaxonomy>();

            var mappings = allTaxonTerms
                .GroupBy(x => x.Term)
                .Select(
                    g => new
                    {
                        AopWikiTermIds = g.Select(t => t.Id).ToList(),
                        Target = g.First().MapToSchema()
                    });

            foreach (var mapping in mappings)
            {
                foreach (var id in mapping.AopWikiTermIds)
                {
                    this._mappedTaxonomiesByAopWikiTermId[id] = mapping.Target;
                }

                this._uniqueTaxonomiesByXmlId.Add(mapping.Target.id, mapping.Target);
            }
        }

        public dataTaxonomy GetByAopWikiId(int aopWikiId)
        {
            return this._mappedTaxonomiesByAopWikiTermId.TryGetValue(aopWikiId, out var taxonomy)
                ? taxonomy
                : throw new KeyNotFoundException($"No mapped taxonomy found for id: {aopWikiId}");
        }

        public IReadOnlyDictionary<string, dataTaxonomy> GetUniqueMappedObjectsByXmlId()
        {
            return this._uniqueTaxonomiesByXmlId;
        }
    }
}