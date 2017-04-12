using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class OrganTerm : IScientificTerm, IAopWikiIdentifiable
    {
        public OrganTerm()
        {
            this.Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OfficialName { get; set; }
        public string Source { get; set; }
        public string SourceId { get; set; }
        public string Synonym { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Term => this.OfficialName;

        public virtual ICollection<Event> Events { get; set; }
    }
}
