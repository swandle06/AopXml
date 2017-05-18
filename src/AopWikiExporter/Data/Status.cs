using System;

namespace AopWikiExporter.Data
{
    public partial class Status : IAopWikiIdentifiable, ITerm
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Term => this.Name;
    }
}
