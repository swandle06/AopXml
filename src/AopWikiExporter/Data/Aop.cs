using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class Aop : IAopWikiIdentifiable
    {
        public Aop()
        {
            this.AopContributors = new HashSet<AopContributor>();
            this.AopLifeStages = new HashSet<AopLifeStage>();
            this.AopLogs = new HashSet<AopLog>();
            this.AopRelationships = new HashSet<AopRelationship>();
            this.AopSexes = new HashSet<AopSex>();
            this.AopStressors = new HashSet<AopStressor>();
            this.AopTaxons = new HashSet<AopTaxon>();
        }

        public int Id { get; set; }
        public string Abstract { get; set; }
        public string ApplicabilityOfTheAop { get; set; }
        public string Authors { get; set; }
        public string Background { get; set; }
        public int? CorrespondingAuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string GraphicalRepresentationImageUid { get; set; }
        public string KeyEventEssentiality { get; set; }
        public sbyte? Legacy { get; set; }
        public string OecdProject { get; set; }
        public int? OecdStatusId { get; set; }
        public string OptionalConsiderations { get; set; }
        public string OverallAssessment { get; set; }
        public string QuantitativeConsiderations { get; set; }
        public string References { get; set; }
        public int? SaaopStatusId { get; set; }
        public string ShortName { get; set; }
        public int? StatusId { get; set; }
        public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserDefinedAo { get; set; }
        public string UserDefinedMie { get; set; }
        public string WeightOfEvidenceSummary { get; set; }

        public virtual ICollection<AopContributor> AopContributors { get; set; }
        public virtual ICollection<AopLifeStage> AopLifeStages { get; set; }
        public virtual ICollection<AopLog> AopLogs { get; set; }
        public virtual ICollection<AopRelationship> AopRelationships { get; set; }
        public virtual ICollection<AopSex> AopSexes { get; set; }
        public virtual ICollection<AopStressor> AopStressors { get; set; }
        public virtual ICollection<AopTaxon> AopTaxons { get; set; }
        public virtual User CorrespondingAuthor { get; set; }
        public virtual OecdStatus OecdStatus { get; set; }
        public virtual SaaopStatus SaaopStatus { get; set; }
    }
}
