using System;

namespace AopWikiExporter.Data
{
    public partial class ChemicalSynonym
    {
        public int Id { get; set; }
        public int? ChemicalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Term { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Chemical Chemical { get; set; }
    }
}
