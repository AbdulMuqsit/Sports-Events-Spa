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


        public SportsEventsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {


        }

        public static SportsEventsDbContext Create()
        {
            return new SportsEventsDbContext();
        }
    }
}