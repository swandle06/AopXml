using System;

namespace AopWikiExporter.Data
{
    public partial class RelationshipLog : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public int? RelationshipId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual Relationship Relationship { get; set; }
        public virtual User User { get; set; }
    }
}
