using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class Chemical : IAopWikiIdentifiable
    {
        public Chemical()
        {
            this.ChemicalSynonyms = new HashSet<ChemicalSynonym>();
            this.StressorChemicals = new HashSet<StressorChemical>();
        }

        public int Id { get; set; }
        public string Casrn { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DsstoxSubstanceId { get; set; }
        public string IndigoInchiKey { get; set; }
        public string JchemInchiKey { get; set; }
        public string PreferredName { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<ChemicalSynonym> ChemicalSynonyms { get; set; }
        public virtual ICollection<StressorChemical> StressorChemicals { get; set; }
    }
}
