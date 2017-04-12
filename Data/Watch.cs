using System;

namespace AopWikiExporter.Data
{
    public partial class Watch : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }
        public int? WatchableId { get; set; }
        public string WatchableType { get; set; }

        public virtual User User { get; set; }
    }
}
