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

        // DbSet for other entities as needed
    }
}
