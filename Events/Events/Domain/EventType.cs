namespace Events.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class EventType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventTypeId { get; set; }
        public string EventTypeName { get; set; }
    }
}
