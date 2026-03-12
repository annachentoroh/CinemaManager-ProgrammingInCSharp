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

        public CinemaHall GetHallById(Guid hallId)
        {
            return Database.CinemaHalls.FirstOrDefault(h => h.Id == hallId);
        }

        public MovieSession GetSessionById(Guid sessionId)
        {
            return Database.MovieSessions.FirstOrDefault(s => s.Id == sessionId);
        }
    }
}