namespace Events.Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Venue
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public int MaxCapacity { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
