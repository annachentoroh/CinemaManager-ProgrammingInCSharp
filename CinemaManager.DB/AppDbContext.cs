using System.Collections.Generic;
using System.Reflection.Emit;
using CinemaManager.Models.Entities;
using CinemaManager.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<MovieSession> MovieSessions { get; set; }

        private readonly string _dbPath;

        public AppDbContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CinemaHall
            modelBuilder.Entity<CinemaHall>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Name).IsRequired().HasMaxLength(200);
                entity.Property(h => h.HallType).HasConversion<string>();
                entity.HasMany(h => h.Sessions)
                      .WithOne()
                      .HasForeignKey(s => s.CinemaHallId)
                      .OnDelete(DeleteBehavior.Cascade); // каскадне видалення
            });

            // MovieSession
            modelBuilder.Entity<MovieSession>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.MovieTitle).IsRequired().HasMaxLength(300);
                entity.Property(s => s.Genre).HasConversion<string>();
            });
        }
    }
}