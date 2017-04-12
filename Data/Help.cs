using System;

namespace AopWikiExporter.Data
{
    public partial class Help : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? InfoLinkedMessageId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ViewLabel { get; set; }

        public virtual InfoLinkedMessage InfoLinkedMessage { get; set; }
    }
}
