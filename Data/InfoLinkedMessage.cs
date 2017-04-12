using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class InfoLinkedMessage : IAopWikiIdentifiable
    {
        public InfoLinkedMessage()
        {
            this.Helps = new HashSet<Help>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? InfoLinkedPageId { get; set; }
        public string Message { get; set; }
        public int? MessageOrder { get; set; }
        public string Section { get; set; }
        public string Subsection { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Help> Helps { get; set; }
        public virtual InfoLinkedPage InfoLinkedPage { get; set; }
    }
}
