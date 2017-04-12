using System;

namespace AopWikiExporter.Data
{
    public partial class AopRelationship : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public int? AopId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? Directness { get; set; }
        public int? EvidenceId { get; set; }
        public int? QuantitativeUnderstandingId { get; set; }
        public int? RelationshipId { get; set; }
        public int? RowOrder { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Aop Aop { get; set; }
        public virtual ConfidenceLevel Evidence { get; set; }
        public virtual ConfidenceLevel QuantitativeUnderstanding { get; set; }
        public virtual Relationship Relationship { get; set; }
    }
}
