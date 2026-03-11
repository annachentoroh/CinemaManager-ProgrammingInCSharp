using CinemaManager.Models.Entities;

namespace CinemaManager.DB
{
    public interface ICinemaRepo
    {
        List<CinemaHall> GetAllHalls();
        CinemaHall GetHallById(Guid hallId);
        MovieSession GetSessionById(Guid sessionId);
        List<MovieSession> GetSessionsByHallId(Guid hallId);
    }
}