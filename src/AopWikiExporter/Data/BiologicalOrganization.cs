using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class BiologicalOrganization : IAopWikiIdentifiable, ITerm
    {
        public BiologicalOrganization()
        {
            this.Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Term { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
