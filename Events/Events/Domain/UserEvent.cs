namespace Events.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserEventId { get; set; }
        public int EventId { get; set; }
        public int GuestId { get; set; }
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
