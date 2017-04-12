using System;

namespace AopWikiExporter.Data
{
    public partial class EventStressor : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EventId { get; set; }
        public string EvidenceText { get; set; }
        public int? StressorId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Event Event { get; set; }
        public virtual Stressor Stressor { get; set; }
    }
}
