using Microsoft.EntityFrameworkCore;

namespace Partytime.Joined.Service.Entities
{
    public class JoinedContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        public JoinedContext(DbContextOptions<JoinedContext> options)
        : base(options)
        {
        }

        public DbSet<Joined> Joined { get; set; } = null!;
    }
}
