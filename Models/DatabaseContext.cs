using Microsoft.EntityFrameworkCore;

namespace Pictionary.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // DbSet for DrawEntry entity
        public DbSet<DrawEntry> DrawEntries { get; set; }

        // DbSet for Guess entity
        public DbSet<Guess> Guesses { get; set; }

        // Optionally, override OnModelCreating method for custom model configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure additional model mappings or configurations here
        }
    }
}
