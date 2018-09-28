namespace Events.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        public int HostId { get; set; }
        public virtual User User { get; set; }
        public int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }
        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public float Cost { get; set; }
        public DateTime DateTime { get; set; }
    }
}
