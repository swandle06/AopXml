using System;

namespace AopWikiExporter.Data
{
    public partial class Comment : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public string AttachedFileUid { get; set; }
        public int? CommentableId { get; set; }
        public string CommentableType { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
