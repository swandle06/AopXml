using System;

namespace AopWikiExporter.Data
{
    public partial class EventLog : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public int? EventId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
