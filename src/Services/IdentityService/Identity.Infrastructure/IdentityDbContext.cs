using Identity.Domain.Common;
using Identity.Domain.Entities;
using Identity.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.Ignore<DomainEvent>();
        }
    }
}
