using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class Stressor : IAopWikiIdentifiable
    {
        public Stressor()
        {
            this.AopStressors = new HashSet<AopStressor>();
            this.EventStressors = new HashSet<EventStressor>();
            this.StressorChemicals = new HashSet<StressorChemical>();
            this.StressorLogs = new HashSet<StressorLog>();
        }

        public int Id { get; set; }
        public string CharacterizationOfExposure { get; set; }
        public string ChemicalDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string References { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<AopStressor> AopStressors { get; set; }
        public virtual ICollection<EventStressor> EventStressors { get; set; }
        public virtual ICollection<StressorChemical> StressorChemicals { get; set; }
        public virtual ICollection<StressorLog> StressorLogs { get; set; }
    }
}
