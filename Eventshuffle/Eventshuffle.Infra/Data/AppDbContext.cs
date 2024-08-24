using Eventshuffle.src.Eventshuffle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eventshuffle.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.DateOptions);
        }
    }
}
