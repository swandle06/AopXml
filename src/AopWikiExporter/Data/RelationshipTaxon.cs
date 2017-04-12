using System;

namespace AopWikiExporter.Data
{
    public partial class RelationshipTaxon : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EvidenceId { get; set; }
        public int? RelationshipId { get; set; }
        public int? TaxonTermId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ConfidenceLevel Evidence { get; set; }
    }
}
