using System;

namespace AopWikiExporter.Data
{
    public partial class EventSex : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EventId { get; set; }
        public int? EvidenceId { get; set; }
        public int? SexTermId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Event Event { get; set; }
        public virtual ConfidenceLevel Evidence { get; set; }
        public virtual SexTerm SexTerm { get; set; }
    }
}
