using CinemaManager.Models.Entities;

namespace CinemaManager.DB
{
    public interface ICinemaRepo
    {
        // Read
        Task<List<CinemaHall>> GetAllHallsAsync();
        Task<CinemaHall?> GetHallByIdAsync(Guid hallId);
        Task<List<MovieSession>> GetSessionsByHallIdAsync(Guid hallId);
        Task<MovieSession?> GetSessionByIdAsync(Guid sessionId);

        // Create
        Task<CinemaHall> AddHallAsync(CinemaHall hall);
        Task<MovieSession> AddSessionAsync(MovieSession session);

        // Update
        Task UpdateHallAsync(CinemaHall hall);
        Task UpdateSessionAsync(MovieSession session);

        // Delete
        Task DeleteHallAsync(Guid hallId);      // каскадно видаляє сеанси
        Task DeleteSessionAsync(Guid sessionId);
    }
}