using System;

namespace AopWikiExporter.Data
{
    public partial class RelationshipSex : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EvidenceId { get; set; }
        public int? RelationshipId { get; set; }
        public int? SexTermId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ConfidenceLevel Evidence { get; set; }
        public virtual Relationship Relationship { get; set; }
        public virtual SexTerm SexTerm { get; set; }
    }
}
