using System;

namespace AopWikiExporter.Data
{
    public partial class EventSubEvent : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EventId { get; set; }
        public int? SubEventId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Event Event { get; set; }
        public virtual SubEvent SubEvent { get; set; }
    }
}
