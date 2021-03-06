﻿using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class SaaopStatus : IAopWikiIdentifiable, ITerm
    {
        public SaaopStatus()
        {
            this.Aops = new HashSet<Aop>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Aop> Aops { get; set; }

        public string Term => this.Name;
    }
}
