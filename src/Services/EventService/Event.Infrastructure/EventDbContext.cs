using Event.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Event.Infrastructure
{
    public class EventDbContext: DbContext
    {

        public DbSet<Event.Domain.Entities.Event> Events { get; set; }
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(
               Assembly.GetAssembly(typeof(EventDbContext)));
            modelBuilder.Ignore<DomainEvent>();
        }
    }
}
