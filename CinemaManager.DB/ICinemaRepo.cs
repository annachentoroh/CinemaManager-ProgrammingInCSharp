using CinemaManager.Models.Entities;

namespace CinemaManager.DB
{
    public interface ICinemaRepo
    {
        List<CinemaHall> GetAllHalls();
        List<MovieSession> GetSessionsByHallId(Guid hallId);
    }
}