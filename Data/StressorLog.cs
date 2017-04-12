﻿using System;

namespace AopWikiExporter.Data
{
    public partial class StressorLog : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public int? StressorId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual Stressor Stressor { get; set; }
        public virtual User User { get; set; }
    }
}
