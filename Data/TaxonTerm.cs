using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class TaxonTerm : IScientificTerm, IAopWikiIdentifiable
    {
        public TaxonTerm()
        {
            this.AopTaxons = new HashSet<AopTaxon>();
            this.EventTaxons = new HashSet<EventTaxon>();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? NcbiId { get; set; }
        public string ScientificTerm { get; set; }
        public string Source { get; set; }
        public string TermClass { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Term => this.ScientificTerm;
        public string SourceId => this.NcbiId?.ToString();
        public string Synonym => null;

        public virtual ICollection<AopTaxon> AopTaxons { get; set; }
        public virtual ICollection<EventTaxon> EventTaxons { get; set; }
    }
}
