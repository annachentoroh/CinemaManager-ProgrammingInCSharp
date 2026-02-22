using CinemaManager.Models.Entities;

namespace CinemaManager.DB
{
    public class CinemaRepo : ICinemaRepo
    {
        public List<CinemaHall> GetAllHalls()
        {
            return Database.CinemaHalls.ToList();
        }

        public List<MovieSession> GetSessionsByHallId(Guid hallId)
        {
            return Database.MovieSessions
                .Where(s => s.CinemaHallId == hallId)
                .ToList();
        }
    }
}