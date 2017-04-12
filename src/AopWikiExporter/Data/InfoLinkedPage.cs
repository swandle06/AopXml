using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class InfoLinkedPage : IAopWikiIdentifiable
    {
        public InfoLinkedPage()
        {
            this.InfoLinkedMessages = new HashSet<InfoLinkedMessage>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? InfoPageId { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<InfoLinkedMessage> InfoLinkedMessages { get; set; }
        public virtual InfoPage InfoPage { get; set; }
    }
}
