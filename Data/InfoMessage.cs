using System;

namespace AopWikiExporter.Data
{
    public partial class InfoMessage : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? InfoPageId { get; set; }
        public string Message { get; set; }
        public int? MessageOrder { get; set; }
        public string Section { get; set; }
        public string Subsection { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual InfoPage InfoPage { get; set; }
    }
}
