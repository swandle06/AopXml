using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class SexTerm : IAopWikiIdentifiable, ITerm
    {
        public SexTerm()
        {
            this.AopSexes = new HashSet<AopSex>();
            this.EventSexes = new HashSet<EventSex>();
            this.RelationshipSexes = new HashSet<RelationshipSex>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Term { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<AopSex> AopSexes { get; set; }
        public virtual ICollection<EventSex> EventSexes { get; set; }
        public virtual ICollection<RelationshipSex> RelationshipSexes { get; set; }
    }
}
