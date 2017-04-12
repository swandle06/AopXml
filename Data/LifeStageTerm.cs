using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class LifeStageTerm : IAopWikiIdentifiable, ITerm
    {
        public LifeStageTerm()
        {
            this.AopLifeStages = new HashSet<AopLifeStage>();
            this.EventLifeStages = new HashSet<EventLifeStage>();
            this.RelationshipLifeStages = new HashSet<RelationshipLifeStage>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Term { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<AopLifeStage> AopLifeStages { get; set; }
        public virtual ICollection<EventLifeStage> EventLifeStages { get; set; }
        public virtual ICollection<RelationshipLifeStage> RelationshipLifeStages { get; set; }
    }
}
