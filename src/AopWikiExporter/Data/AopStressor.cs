using System;

namespace AopWikiExporter.Data
{
    public partial class AopStressor : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public int? AopId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EvidenceId { get; set; }
        public string EvidenceText { get; set; }
        public int? StressorId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Aop Aop { get; set; }
        public virtual ConfidenceLevel Evidence { get; set; }
        public virtual Stressor Stressor { get; set; }
    }
}
