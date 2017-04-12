using System;

namespace AopWikiExporter.Data
{
    public partial class Assignment : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? RoleId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }
    }
}
