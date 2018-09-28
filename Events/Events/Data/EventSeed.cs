namespace Events.Data
{
    using Events.Domain;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventSeed
    {
        public static async Task SeedAsync(EventContext context)
        {
            context.Database.Migrate();
            if (!context.EventTypes.Any())
            {
                context.EventTypes.AddRange
                    (GetPreconfiguredEventTypes());
                await context.SaveChangesAsync();
            }
            if (!context.Venues.Any())
            {
                context.Venues.AddRange
                    (GetPreConfiguredVenues());
                await context.SaveChangesAsync();
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange
                    (GetPreconfiguredUsers());
                await context.SaveChangesAsync();
            }
            if (!context.Events.Any())
            {
                context.Events.AddRange
                    (GetPreConfiguredEvents());
                await context.SaveChangesAsync();
            }
            if (!context.UserEvents.Any())
            {
                context.UserEvents.AddRange
                    (GetPreConfiguredUserEvents());
                await context.SaveChangesAsync();
            }

        }

        static IEnumerable<EventType> GetPreconfiguredEventTypes()
        {
            return new List<EventType>()
            {
                new EventType() { EventTypeName = "Social"},
                new EventType() { EventTypeName = "Networking" },
                new EventType() { EventTypeName = "Tech" },
                new EventType() { EventTypeName = "Travel" },
                new EventType() { EventTypeName = "Music" },

            };
        }

        static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User() { UserName="omkarmkulkarni", EmailAddress="omkar7705@gmail.com", FirstName="Omkar", LastName="Kulkarni", Password="pwdIsFake" },
                new User() { UserName="alberteinstein", EmailAddress="albert@einstein.com", FirstName="Albert", LastName="Einstein", Password="e=mc2" },
                new User() { UserName="galileogalilei", EmailAddress="galelio@telescope.com", FirstName="Galelio", LastName="Galilei", Password="earthisround" },
                new User() { UserName="thomasalvaedison", EmailAddress="thomasedison@bulb.com", FirstName="Thomas", LastName="Edison", Password="electricBulb" },
                new User() { UserName="isaacnewton", EmailAddress="apple@gravity.com", FirstName="Isaac", LastName="Newton", Password="inertia12!@" },

            };
        }

        static IEnumerable<Venue> GetPreConfiguredVenues()
        {
            return new List<Venue>()
            {
                new Venue() { VenueName = "Seattle Public Library", Address = "1000 Fourth Ave.", City = "Seattle", State = "WA", ZipCode = "98109", MaxCapacity = 250},
                new Venue() { VenueName = "Four Seasons", Address = "415 Westlake Avenue North", City = "Seattle", State = "WA", ZipCode = "98109", MaxCapacity = 250},
                new Venue() { VenueName = "Hotel Sorrento", Address = "900 Madison Street", City = "Seattle", State = "WA", ZipCode = "98104", MaxCapacity = 250},
                new Venue() { VenueName = "Renaissance Seattle Hotel", Address = "515 Madison Street", City = "Seattle", State = "WA", ZipCode = "98104", MaxCapacity = 250},
                new Venue() { VenueName = "Hotel Theodore", Address = "Hotel Theodore", City = "Seattle", State = "WA", ZipCode = "98109", MaxCapacity = 250},

            };
        }

        static IEnumerable<Event> GetPreConfiguredEvents()
        {
            return new List<Event>()
            {
                //Social
                new Event() { HostId=1,VenueId=5,EventTypeId=1, DateTime=DateTime.Parse("2018-01-17T23:00:30"), Description = "Indoor/Outdoor Brunch.", EventName = "Sunday Social", Cost = 15, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/1" },
                new Event() { HostId=2,VenueId=4,EventTypeId=1, DateTime=DateTime.Parse("2018-02-17T23:00:30"), Description = "Escape Room Enthusiasts, Room Owners, Puzzle People, and friends are welcome. Stop by for a bite, leave with some new teammates! ", EventName = "Guru Game Night + Social", Cost = 0, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/2" },
                new Event() { HostId=3,VenueId=2,EventTypeId=1, DateTime=DateTime.Parse("2018-03-17T23:00:30"), Description = "Come and play with other puppies!", EventName = "Puppy Social", Cost = 5, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/3" },
                new Event() { HostId=4,VenueId=3,EventTypeId=1, DateTime=DateTime.Parse("2018-04-17T23:00:30"), Description = "What is dead may never die! Raise your poisoned glasses as we celebrate SEVEN seasons of wedding mishaps, family squabbles, and good guys always winning at Game of Thrones Trivia", EventName = "Game of Thrones Trivia", Cost = 10, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/4" },
                new Event() { HostId=5,VenueId=1,EventTypeId=1, DateTime=DateTime.Parse("2018-05-17T23:00:30"), Description = "By popular demand, we're excited to be organizing our third Creators Social meetup!", EventName = "The Creators Social Brunch", Cost = 0, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/5" },
                //Networking
                new Event() { HostId=1,VenueId=5,EventTypeId=2, DateTime=DateTime.Parse("2018-06-17T23:00:30"), Description = "Come by after work to join our networking event! We'll provide light hors d'oeuvres.", EventName = "Networking Event", Cost = 10, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/6" },
                new Event() { HostId=2,VenueId=4,EventTypeId=2, DateTime=DateTime.Parse("2018-07-17T23:00:30"), Description = "Monthly events are free community events open to entrepreneurs and investors.", EventName = "Entrepreneurs Roundtable", Cost = 0, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/7"  },
                new Event() { HostId=3,VenueId=2,EventTypeId=2, DateTime=DateTime.Parse("2018-08-17T23:00:30"), Description = "Join us for our Meetup Networking social event for professionals", EventName = "Young Professionals Networking Social", Cost = 0, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/8" },
                new Event() { HostId=4,VenueId=3,EventTypeId=2, DateTime=DateTime.Parse("2018-09-17T23:00:30"), Description = "A seminar including a series of panel discussions, key-note speakers, and networking opportunities.", EventName = "Cyber Day", Cost = 20, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/9" },
                new Event() { HostId=5,VenueId=1,EventTypeId=2, DateTime=DateTime.Parse("2018-10-17T23:00:30"), Description = "Join fellow advancement professionals for a day of networking and professional development.", EventName = "Network Roundtable", Cost = 0, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/10" },
                //Tech
                new Event() { HostId=1,VenueId=5,EventTypeId=3, DateTime=DateTime.Parse("2018-11-17T23:00:30"), Description = "Enjoy drinks and light bites as you network", EventName = "Tech Mixer", Cost = 15, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/11" },
                new Event() { HostId=2,VenueId=4,EventTypeId=3, DateTime=DateTime.Parse("2018-12-17T23:00:30"), Description = "Learn to Develop a Successful Internet of Things Startup", EventName = "Develop a Successful IoT Tech", Cost = 5, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/12"  },
                new Event() { HostId=3,VenueId=2,EventTypeId=3, DateTime=DateTime.Parse("2019-01-17T23:00:30"), Description = "Learn how to code websites or mobile applications", EventName = "Free Tech Training for Women", Cost = 0, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/13" },
                new Event() { HostId=4,VenueId=3,EventTypeId=3, DateTime=DateTime.Parse("2019-02-17T23:00:30"), Description = "Learn how to develop AI Hardware and AI Software.", EventName = "AI Workshop", Cost = 20, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/14" },
                new Event() { HostId=5,VenueId=1,EventTypeId=3, DateTime=DateTime.Parse("2019-03-17T23:00:30"), Description = "This event is open to all tech enthusiasts of all levels.", EventName = "Hack Night", Cost = 50, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/15" },
               //Travel
                new Event() { HostId=1,VenueId=5,EventTypeId=4, DateTime=DateTime.Parse("2019-04-17T23:00:30"), Description = "Royal Carribean on the Celebrity Solstice", EventName = "The Ultimate Alaska Cruise", Cost = 5, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/16" },
                new Event() { HostId=2,VenueId=4,EventTypeId=4, DateTime=DateTime.Parse("2019-05-17T23:00:30"), Description = "Meet in Paris.", EventName = "Paris", Cost = 10, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/17"  },
                new Event() { HostId=3,VenueId=2,EventTypeId=4, DateTime=DateTime.Parse("2019-06-17T23:00:30"), Description = "Expedition cruise", EventName = "Travel Geeks", Cost = 0, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/18" },
                new Event() { HostId=4,VenueId=3,EventTypeId=4, DateTime=DateTime.Parse("2019-07-17T23:00:30"), Description = "Join guest speakers discuss the wonders of China.", EventName = "China", Cost = 20, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/19" },
                new Event() { HostId=5,VenueId=1,EventTypeId=4, DateTime=DateTime.Parse("2019-08-17T23:00:30"), Description = "Share and discuss knowledge and best practice around innovation in tour guiding", EventName = "The National Tour Guiding Conference", Cost = 30, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/20" },
                //Music
                new Event() { HostId=3,VenueId=5,EventTypeId=5, DateTime=DateTime.Parse("2019-09-17T23:00:30"), Description = "A collaboration between classical music artists.", EventName = "Chamber Music in Seattle", Cost = 20, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/21" },
                new Event() { HostId=3,VenueId=4,EventTypeId=5, DateTime=DateTime.Parse("2019-10-17T23:00:30"), Description = "This practical weekend course offers a rare and detailed insight into the tools, techniques and best practice of writing commercial pop songs", EventName = "Pop Music Songwriting", Cost= 10, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/22" },
                new Event() { HostId=3,VenueId=2,EventTypeId=5, DateTime=DateTime.Parse("2019-11-17T23:00:30"), Description = "Concert series allows the audience to enjoy the music and art in an entirely new, exciting and indulgent way.", EventName = "Music and Art of the Night", Cost = 0, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/23" },
                new Event() { HostId=3,VenueId=3,EventTypeId=5, DateTime=DateTime.Parse("2019-12-17T23:00:30"), Description = "Learn the fundamentals of playing the guitar, strum along to popular songs and improve your understanding of music. You don't need any experience.", EventName = "Music - Guitar - 2018-19", Cost = 15, PictureUrl = "http://externaleventbaseurltobereplaced/api/pic/24" },
             };
        }
        static IEnumerable<UserEvent> GetPreConfiguredUserEvents()
        {
            return new List<UserEvent>()
            {
                new UserEvent() {EventId=1, GuestId=1},
                new UserEvent() {EventId=2, GuestId=1},
                new UserEvent() {EventId=3, GuestId=1},
                new UserEvent() {EventId=4, GuestId=1},
                new UserEvent() {EventId=5, GuestId=1},
                new UserEvent() {EventId=1, GuestId=2},
                new UserEvent() {EventId=2, GuestId=2},
                new UserEvent() {EventId=3, GuestId=2},
                new UserEvent() {EventId=4, GuestId=2},
                new UserEvent() {EventId=5, GuestId=2},
                new UserEvent() {EventId=1, GuestId=3},
                new UserEvent() {EventId=2, GuestId=3},
                new UserEvent() {EventId=3, GuestId=3},
                new UserEvent() {EventId=4, GuestId=3},
                new UserEvent() {EventId=5, GuestId=3},
           };
        }

    }
}
