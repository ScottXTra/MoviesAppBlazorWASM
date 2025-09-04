using Microsoft.EntityFrameworkCore;
using MoviesApp.Shared.Models;

namespace MoviesApp.Server.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Mission> Missions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Genre).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ReleaseDate).IsRequired().HasColumnType("date");
                entity.Property(e => e.BoxOfficeSales).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Mission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Destination).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LaunchDate).IsRequired().HasColumnType("date");
                entity.Property(e => e.Status).HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}