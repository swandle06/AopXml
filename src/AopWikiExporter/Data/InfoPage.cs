using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class InfoPage : IAopWikiIdentifiable
    {
        public InfoPage()
        {
            this.InfoLinkedPages = new HashSet<InfoLinkedPage>();
            this.InfoMessages = new HashSet<InfoMessage>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<InfoLinkedPage> InfoLinkedPages { get; set; }
        public virtual ICollection<InfoMessage> InfoMessages { get; set; }
    }
}
