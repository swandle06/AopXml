using System;
using System.Collections.Generic;

namespace AopWikiExporter.Data
{
    public partial class SubEvent : IAopWikiIdentifiable
    {
        public SubEvent()
        {
            this.EventSubEvents = new HashSet<EventSubEvent>();
        }

        public int Id { get; set; }
        public int? BiologicalActionId { get; set; }
        public int? BiologicalObjectId { get; set; }
        public int? BiologicalProcessId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<EventSubEvent> EventSubEvents { get; set; }
        public virtual BiologicalAction BiologicalAction { get; set; }
        public virtual BiologicalObject BiologicalObject { get; set; }
        public virtual BiologicalProcess BiologicalProcess { get; set; }
    }
}
