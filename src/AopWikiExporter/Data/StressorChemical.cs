using System;

namespace AopWikiExporter.Data
{
    public partial class StressorChemical : IAopWikiIdentifiable
    {
        public int Id { get; set; }
        public int? ChemicalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? StressorId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserTerm { get; set; }

        public virtual Chemical Chemical { get; set; }
        public virtual Stressor Stressor { get; set; }
    }
}
