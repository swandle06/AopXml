using System;

namespace AopWikiExporter.Data
{
    public partial class AopContributor : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public int? AopId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual Aop Aop { get; set; }
        public virtual User User { get; set; }
    }
}
