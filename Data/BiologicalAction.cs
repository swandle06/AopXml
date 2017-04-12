using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class BiologicalAction : IScientificTerm, IAopWikiIdentifiable
    {
        public BiologicalAction()
        {
            this.SubEvents = new HashSet<SubEvent>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Source { get; set; }
        public string SourceId { get; set; }
        public string Term { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Synonym => null;

        public virtual ICollection<SubEvent> SubEvents { get; set; }
    }
}
