using System;

namespace AopWikiExporter.Data
{
    public partial class EventLifeStage : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? EventId { get; set; }
        public int? EvidenceId { get; set; }
        public int? LifeStageTermId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Event Event { get; set; }
        public virtual ConfidenceLevel Evidence { get; set; }
        public virtual LifeStageTerm LifeStageTerm { get; set; }
    }
}
