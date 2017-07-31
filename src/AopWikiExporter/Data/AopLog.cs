using System;

namespace AopWikiExporter.Data
{
    public partial class AopLog : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public int? AopId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual Aop Aop { get; set; }
        public virtual User User { get; set; }
    }
}
