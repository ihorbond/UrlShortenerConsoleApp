using Microsoft.EntityFrameworkCore;

using UrlShortener.Models;

namespace UrlShortener.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "UrlShortener");
        }

        public DbSet<Url> Urls{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>(entity =>
            {
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.LongUrl).IsRequired().HasMaxLength(500);
                entity.Property(p => p.ShortUrl).IsRequired().HasMaxLength(50);
                entity.Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }
}
