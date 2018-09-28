namespace Events.Data
{
    using Events.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Linq;

    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions options): base (options)
        {

        }

        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);
            builder.Entity<EventType>(ConfigureEventType);
            builder.Entity<Venue>(ConfigureVenue);
            builder.Entity<User>(ConfigureUser);
            builder.Entity<Event>(ConfigureEvent);
            builder.Entity<UserEvent>(ConfigureUserEvent);
        }

        private void ConfigureUserEvent(EntityTypeBuilder<UserEvent> builder)
        {
            builder.ToTable("UserEvent");
            builder.HasOne(ue => ue.Event).WithMany().HasForeignKey(ue => ue.EventId);
            builder.HasOne(ue => ue.User).WithMany().HasForeignKey(ue => ue.GuestId);
            builder.Property(ue => ue.UserEventId).ForSqlServerUseSequenceHiLo("event_id_hilo").IsRequired().ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            //builder.Property(v => v.VenueName).IsRequired().HasMaxLength(100);
        }

        private void ConfigureEventType(EntityTypeBuilder<EventType> builder)
        {
            builder.ToTable("EventType");
            builder.Property(et => et.EventTypeId).ForSqlServerUseSequenceHiLo("event_id_hilo").IsRequired().ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            builder.Property(et => et.EventTypeName).IsRequired();
        }

        private void ConfigureVenue(EntityTypeBuilder<Venue> builder)
        {
            builder.ToTable("Venue");
            builder.Property(v => v.VenueId).ForSqlServerUseSequenceHiLo("event_id_hilo").IsRequired().ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            builder.Property(v => v.VenueName).IsRequired();
            builder.Property(v => v.Address).IsRequired();
            builder.Property(v => v.City).IsRequired();
            builder.Property(v => v.State).IsRequired();
            builder.Property(v => v.ZipCode).IsRequired();
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(u => u.UserId).ForSqlServerUseSequenceHiLo("event_id_hilo").IsRequired().ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.EmailAddress).IsRequired();

        }

        private void ConfigureEvent(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.Property(e => e.EventId).ForSqlServerUseSequenceHiLo ( "event_id_hilo" ).IsRequired().ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            builder.Property(e => e.EventName).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.DateTime).IsRequired();
            builder.Property(e => e.PictureUrl).IsRequired(false);
            builder.Property(e => e.Cost).IsRequired();

            builder.HasOne(e => e.EventType).WithMany().HasForeignKey(e => e.EventTypeId);
            builder.HasOne(e => e.Venue).WithMany().HasForeignKey(e => e.VenueId);
            builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.HostId);
        }
    }
}
