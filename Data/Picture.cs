using System;

namespace AopWikiExporter.Data
{
    public partial class Picture : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public int AopId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageName { get; set; }
        public string ImageTitle { get; set; }
        public string ImageUid { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
