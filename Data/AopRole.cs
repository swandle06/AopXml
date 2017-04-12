using System;

namespace AopWikiExporter.Data
{
    public partial class AopRole : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Role { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
