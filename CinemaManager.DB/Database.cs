using CinemaManager.Models.Entities;
using CinemaManager.Models.Enums;

namespace CinemaManager.DB
{
    internal static class Database
    {
        public static List<CinemaHall> CinemaHalls { get; } = new List<CinemaHall>();
        public static List<MovieSession> MovieSessions { get; } = new List<MovieSession>();


        static Database()
        {
            Data();
        }

        public static void Data() 
        {
            var hall1 = new CinemaHall(Guid.NewGuid(), "IMAX hall", 120, HallType.IMAX);
            var hall2 = new CinemaHall(Guid.NewGuid(), "ThreeD hall", 80, HallType.ThreeD);
            var hall3 = new CinemaHall(Guid.NewGuid(), "Private hall", 60, HallType.TwoD);

            CinemaHalls.Add(hall1);
            CinemaHalls.Add(hall2);
            CinemaHalls.Add(hall3);

            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "Stormy Pass", Genre.Drama, 2026, DateTime.Today.AddHours(10), 160));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "Maid", Genre.Thriller, 2026, DateTime.Today.AddHours(14), 195));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "1+1", Genre.Comedy, 1999, DateTime.Today.AddHours(18), 90));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "Joker", Genre.Drama, 2019, DateTime.Today.AddHours(21), 120));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "It", Genre.Horror, 2019, DateTime.Today.AddHours(20), 135));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "Captain America", Genre.Action, 2000, DateTime.Today.AddHours(22), 95));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "Avatar", Genre.Action, 2026, DateTime.Today.AddHours(10), 160));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "Zoopolis", Genre.Cartoon, 1997, DateTime.Today.AddHours(14), 195));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "Shrek", Genre.Cartoon, 2001, DateTime.Today.AddHours(18), 90));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall1.Id, "Goat", Genre.Cartoon, 2019, DateTime.Today.AddHours(21), 120));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall2.Id, "Avengers", Genre.Action, 2019, DateTime.Today.AddHours(20), 135));
            MovieSessions.Add(new MovieSession(Guid.NewGuid(), hall2.Id, "Spider Man", Genre.Action, 2000, DateTime.Today.AddHours(22), 95));
        }
    }
}

