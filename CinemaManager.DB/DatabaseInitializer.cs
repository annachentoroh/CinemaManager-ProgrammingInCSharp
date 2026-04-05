using CinemaManager.Models.Entities;
using CinemaManager.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.DB
{
    /// <summary>
    /// Ініціалізує БД і заповнює початковими даними лише при першому запуску.
    /// </summary>
    public class DatabaseInitializer
    {
        private readonly DbContextFactory _factory;

        public DatabaseInitializer(DbContextFactory factory)
        {
            _factory = factory;
        }

        public async Task InitializeAsync()
        {
            await using var db = _factory.Create();
            await db.Database.EnsureCreatedAsync();

            // Заповнення лише якщо БД порожня
            if (await db.CinemaHalls.AnyAsync())
                return;

            var hall1 = new CinemaHall(Guid.NewGuid(), "IMAX Hall", 120, HallType.IMAX);
            var hall2 = new CinemaHall(Guid.NewGuid(), "ThreeD Hall", 80, HallType.ThreeD);
            var hall3 = new CinemaHall(Guid.NewGuid(), "Private Hall", 60, HallType.TwoD);

            db.CinemaHalls.AddRange(hall1, hall2, hall3);

            var sessions = new List<MovieSession>
            {
                new(Guid.NewGuid(), hall1.Id, "Stormy Pass", Genre.Drama, 2026, DateTime.Today.AddHours(10), 160),
                new(Guid.NewGuid(), hall1.Id, "Maid", Genre.Thriller, 2026, DateTime.Today.AddHours(14), 195),
                new(Guid.NewGuid(), hall1.Id, "1+1", Genre.Comedy, 1999, DateTime.Today.AddHours(18), 90),
                new(Guid.NewGuid(), hall1.Id, "Joker", Genre.Drama, 2019, DateTime.Today.AddHours(21), 120),
                new(Guid.NewGuid(), hall1.Id, "It", Genre.Horror, 2019, DateTime.Today.AddHours(20), 135),
                new(Guid.NewGuid(), hall1.Id, "Captain America", Genre.Action, 2000, DateTime.Today.AddHours(22), 95),
                new(Guid.NewGuid(), hall1.Id, "Avatar", Genre.Action, 2026, DateTime.Today.AddHours(10), 160),
                new(Guid.NewGuid(), hall1.Id, "Zootopia", Genre.Cartoon, 2016, DateTime.Today.AddHours(14), 108),
                new(Guid.NewGuid(), hall1.Id, "Shrek", Genre.Cartoon, 2001, DateTime.Today.AddHours(18), 90),
                new(Guid.NewGuid(), hall1.Id, "The Goat", Genre.Cartoon, 2019, DateTime.Today.AddHours(21), 120),
                new(Guid.NewGuid(), hall2.Id, "Avengers", Genre.Action, 2019, DateTime.Today.AddHours(20), 135),
                new(Guid.NewGuid(), hall2.Id, "Spider-Man", Genre.Action, 2000, DateTime.Today.AddHours(22), 95),
            };

            db.MovieSessions.AddRange(sessions);
            await db.SaveChangesAsync();
        }
    }
}