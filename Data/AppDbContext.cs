using Microsoft.EntityFrameworkCore;

namespace Orderly.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } // DbSet dla tabeli Users

        // Konstruktor przyjmujący DbContextOptions<AppDbContext>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Opcjonalnie: Możesz dodać metodę OnModelCreating, aby skonfigurować modele
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Możesz skonfigurować dodatkowe ograniczenia i relacje tutaj
        }
    }
}
