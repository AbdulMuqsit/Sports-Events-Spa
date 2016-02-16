using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SportsEvents.Common.Entities;

namespace SportsEvents.EntityFramework
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class SportsEventsDbContext : IdentityDbContext<User>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }


        public SportsEventsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {


        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ComplexType<ContactDetails>();
            modelBuilder.ComplexType<Address>();
            modelBuilder.Entity<Event>().HasMany(e => e.BookmarkerVisitors).WithMany(e => e.BookmarkedEvents).Map(config => config.MapLeftKey("BookmarkedEventId").MapRightKey("BookMarkerId").ToTable("Bookmarks_User_Event"));
            modelBuilder.Entity<Event>().HasMany(e => e.RegisteredVisitors).WithMany(e => e.RegisteredEvents).Map(e => e.MapLeftKey("RegisteredUserId").MapRightKey("RegisteredEventId").ToTable("Registrations_User_Event"));
            modelBuilder.Entity<Event>().HasMany(e => e.RegisterRequestVisitors).WithMany(e => e.RegistrationRequests).Map(e => e.MapLeftKey("RegisterRequestVisitorId").MapRightKey("RegisterationRequestId").ToTable("RegistrationRequests_User_Event"));
            modelBuilder.Entity<Event>().HasMany(e => e.ClickerUsers).WithMany(e => e.ClickedEvents).Map(e => e.MapLeftKey("ClickerUserId").MapRightKey("ClickedEventId").ToTable("ClickedEvents_User_Events"));

        }

        public static SportsEventsDbContext Create()
        {
            return new SportsEventsDbContext();
        }
    }
}