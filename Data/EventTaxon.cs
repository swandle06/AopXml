using System;

namespace AopWikiExporter.Data
{
    public partial class EventTaxon : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EventId { get; set; }
        public int? EvidenceId { get; set; }
        public int? TaxonTermId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Event Event { get; set; }
        public virtual ConfidenceLevel Evidence { get; set; }
        public virtual TaxonTerm TaxonTerm { get; set; }
    }
}
