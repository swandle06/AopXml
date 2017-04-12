using System;

namespace AopWikiExporter.Data
{
    public partial class TermRequest : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Justification { get; set; }
        public string ScientificTerm { get; set; }
        public string Term { get; set; }
        public int? TermType { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
