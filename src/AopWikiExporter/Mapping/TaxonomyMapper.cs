﻿using System;
using System.Collections.Generic;
using System.Linq;
using AopWikiExporter.Data;

namespace AopWikiExporter.Mapping
{
    class TaxonomyMapper
    {
        readonly IDictionary<int, dataTaxonomy> _mappedTaxonomiesByAopWikiTermId;
        readonly List<dataTaxonomy> _uniqueTaxonomies;

        public TaxonomyMapper(IQueryable<TaxonTerm> allTaxonTerms)
        {
            if (allTaxonTerms == null) throw new ArgumentNullException(nameof(allTaxonTerms));

            this._mappedTaxonomiesByAopWikiTermId = new Dictionary<int, dataTaxonomy>();
            this._uniqueTaxonomies = new List<dataTaxonomy>();

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

                this._uniqueTaxonomies.Add(mapping.Target);
            }
        }

        public dataTaxonomy GetByAopWikiId(int aopWikiId)
        {
            return this._mappedTaxonomiesByAopWikiTermId.TryGetValue(aopWikiId, out var taxonomy)
                ? taxonomy
                : throw new KeyNotFoundException($"No mapped taxonomy found for id: {aopWikiId}");
        }

        public IReadOnlyCollection<dataTaxonomy> GetUniqueMappedObjects()
        {
            return this._uniqueTaxonomies;
        }
    }
}