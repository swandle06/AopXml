using System;

namespace AopWikiExporter.Data
{
    public partial class Version : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Event { get; set; }
        public int ItemId { get; set; }
        public string ItemType { get; set; }
        public string Object { get; set; }
        public string Whodunnit { get; set; }
    }
}
