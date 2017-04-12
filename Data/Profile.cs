using System;

namespace AopWikiExporter.Data
{
    public partial class Profile : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public string Affiliation { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TimeZone { get; set; }
        public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
    }
}
