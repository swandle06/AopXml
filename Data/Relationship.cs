using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class Relationship : IAopWikiIdentifiable
    {
        public Relationship()
        {
            this.AopRelationships = new HashSet<AopRelationship>();
            this.RelationshipLifeStages = new HashSet<RelationshipLifeStage>();
            this.RelationshipLogs = new HashSet<RelationshipLog>();
            this.RelationshipSexes = new HashSet<RelationshipSex>();
        }

        public int Id { get; set; }
        public string BiologicalPlausibility { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? DownstreamEventId { get; set; }
        public string EmpiricalSupport { get; set; }
        public string HowItWorks { get; set; }
        public string QuantitativeUnderstanding { get; set; }
        public string References { get; set; }
        public string TaxonEvidence { get; set; }
        public string Uncertainties { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UpstreamEventId { get; set; }
        public string WeightOfEvidence { get; set; }

        public virtual ICollection<AopRelationship> AopRelationships { get; set; }
        public virtual ICollection<RelationshipLifeStage> RelationshipLifeStages { get; set; }
        public virtual ICollection<RelationshipLog> RelationshipLogs { get; set; }
        public virtual ICollection<RelationshipSex> RelationshipSexes { get; set; }
    }
}
