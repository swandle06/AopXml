using System;

namespace AopWikiExporter.Data
{
    public partial class Snapshot : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public int AopId { get; set; }
        public int? Archive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Creator { get; set; }
        public string HtmlFile { get; set; }
        public string Notes { get; set; }
        public string PdfFile { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
