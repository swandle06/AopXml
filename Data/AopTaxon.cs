using System;

namespace AopWikiExporter.Data
{
    public partial class AopTaxon : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public int? AopId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EvidenceId { get; set; }
        public int? TaxonTermId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Aop Aop { get; set; }
        public virtual ConfidenceLevel Evidence { get; set; }
        public virtual TaxonTerm TaxonTerm { get; set; }
    }
}
