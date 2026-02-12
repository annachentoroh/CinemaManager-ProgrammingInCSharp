using CinemaManager.Models.Entities;
using CinemaManager.Models.Enums;

namespace CinemaManager.DB
{
    internal static class Database
    {
        internal static List<CinemaHall> CinemaHalls = new()
        {
            new CinemaHall(Guid.NewGuid(), "IMAX hall", 120, HallType.IMAX),
            new CinemaHall(Guid.NewGuid(), "ThreeD hall", 80, HallType.ThreeD),
            new CinemaHall(Guid.NewGuid(), "Private hall", 60, HallType.TwoD)
        };

        internal static List<MovieSession> MovieSessions = new();

        static Database()
        {
            var hall1 = CinemaHalls[0].Id;
            var hall2 = CinemaHalls[1].Id;

            MovieSessions.AddRange(new[]
            {
                new MovieSession(Guid.NewGuid(), hall1, "Stormy Pass", Genre.Drama, 2026, DateTime.Today.AddHours(10), 160),
                new MovieSession(Guid.NewGuid(), hall1, "Maid", Genre.Thriller, 2026, DateTime.Today.AddHours(14), 195),
                new MovieSession(Guid.NewGuid(), hall1, "1+1", Genre.Comedy, 1999, DateTime.Today.AddHours(18), 90),
                new MovieSession(Guid.NewGuid(), hall1, "Joker", Genre.Drama, 2019, DateTime.Today.AddHours(21), 120),
                new MovieSession(Guid.NewGuid(), hall1, "It", Genre.Horror, 2019, DateTime.Today.AddHours(20), 135),
                new MovieSession(Guid.NewGuid(), hall1, "Captain America", Genre.Action, 2000, DateTime.Today.AddHours(22), 95),
                new MovieSession(Guid.NewGuid(), hall1, "Avatar", Genre.Action, 2026, DateTime.Today.AddHours(10), 160),
                new MovieSession(Guid.NewGuid(), hall1, "Zoopolis", Genre.Cartoon, 1997, DateTime.Today.AddHours(14), 195),
                new MovieSession(Guid.NewGuid(), hall1, "Shrek", Genre.Cartoon, 2001, DateTime.Today.AddHours(18), 90),
                new MovieSession(Guid.NewGuid(), hall1, "Goat", Genre.Cartoon, 2019, DateTime.Today.AddHours(21), 120),
                new MovieSession(Guid.NewGuid(), hall2, "Avengers", Genre.Action, 2019, DateTime.Today.AddHours(20), 135),
                new MovieSession(Guid.NewGuid(), hall2, "Spider Man", Genre.Action, 2000, DateTime.Today.AddHours(22), 95)
            });
        }
    }
}

