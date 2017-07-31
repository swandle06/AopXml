using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class Event : IAopWikiIdentifiable
    {
        public Event()
        {
            this.EventLifeStages = new HashSet<EventLifeStage>();
            this.EventLogs = new HashSet<EventLog>();
            this.EventSexes = new HashSet<EventSex>();
            this.EventStressors = new HashSet<EventStressor>();
            this.EventSubEvents = new HashSet<EventSubEvent>();
            this.EventTaxons = new HashSet<EventTaxon>();
        }

        public int Id { get; set; }
        public int? BiologicalOrganizationId { get; set; }
        public int? CellTermId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Definition { get; set; }
        public string EvidenceForChemicalInitiation { get; set; }
        public string ExamplesUsingAo { get; set; }
        public string HowItWorks { get; set; }
        public string MeasuredOrDetected { get; set; }
        public int? OrganTermId { get; set; }
        public string References { get; set; }
        public string ShortName { get; set; }
        public string SupportingTaxEvidence { get; set; }
        public string Title { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<EventLifeStage> EventLifeStages { get; set; }
        public virtual ICollection<EventLog> EventLogs { get; set; }
        public virtual ICollection<EventSex> EventSexes { get; set; }
        public virtual ICollection<EventStressor> EventStressors { get; set; }
        public virtual ICollection<EventSubEvent> EventSubEvents { get; set; }
        public virtual ICollection<EventTaxon> EventTaxons { get; set; }
        public virtual BiologicalOrganization BiologicalOrganization { get; set; }
        public virtual CellTerm CellTerm { get; set; }
        public virtual OrganTerm OrganTerm { get; set; }
    }
}
