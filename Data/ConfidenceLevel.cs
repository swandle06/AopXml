using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class ConfidenceLevel : IAopWikiIdentifiable, ITerm
    {
        public ConfidenceLevel()
        {
            this.AopEvents = new HashSet<AopEvent>();
            this.AopLifeStages = new HashSet<AopLifeStage>();
            this.AopRelationshipsEvidence = new HashSet<AopRelationship>();
            this.AopRelationshipsQuantitativeUnderstanding = new HashSet<AopRelationship>();
            this.AopSexes = new HashSet<AopSex>();
            this.AopStressors = new HashSet<AopStressor>();
            this.AopTaxons = new HashSet<AopTaxon>();
            this.EventLifeStages = new HashSet<EventLifeStage>();
            this.EventSexes = new HashSet<EventSex>();
            this.EventTaxons = new HashSet<EventTaxon>();
            this.RelationshipLifeStages = new HashSet<RelationshipLifeStage>();
            this.RelationshipSexes = new HashSet<RelationshipSex>();
            this.RelationshipTaxons = new HashSet<RelationshipTaxon>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Term { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<AopEvent> AopEvents { get; set; }
        public virtual ICollection<AopLifeStage> AopLifeStages { get; set; }
        public virtual ICollection<AopRelationship> AopRelationshipsEvidence { get; set; }
        public virtual ICollection<AopRelationship> AopRelationshipsQuantitativeUnderstanding { get; set; }
        public virtual ICollection<AopSex> AopSexes { get; set; }
        public virtual ICollection<AopStressor> AopStressors { get; set; }
        public virtual ICollection<AopTaxon> AopTaxons { get; set; }
        public virtual ICollection<EventLifeStage> EventLifeStages { get; set; }
        public virtual ICollection<EventSex> EventSexes { get; set; }
        public virtual ICollection<EventTaxon> EventTaxons { get; set; }
        public virtual ICollection<RelationshipLifeStage> RelationshipLifeStages { get; set; }
        public virtual ICollection<RelationshipSex> RelationshipSexes { get; set; }
        public virtual ICollection<RelationshipTaxon> RelationshipTaxons { get; set; }
    }
}
