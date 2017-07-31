using System;

namespace AopWikiExporter.Data
{
    public partial class AopEvent : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public int? AopId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EssentialityId { get; set; }
        public int? EventId { get; set; }
        public int? RowOrder { get; set; }
        public string Type { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ConfidenceLevel Essentiality { get; set; }
    }
}
