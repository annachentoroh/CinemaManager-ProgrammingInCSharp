using CinemaManager.Services.DTO;

namespace CinemaManager.Services
{
    public interface ICinemaService
    {
        // Halls
        Task<IEnumerable<CinemaHallListDTO>> GetAllHallsAsync();
        Task<CinemaHallDetailsDTO?> GetHallDetailsAsync(Guid hallId);
        Task<CinemaHallListDTO> CreateHallAsync(CinemaHallCreateDTO dto);
        Task UpdateHallAsync(CinemaHallUpdateDTO dto);
        Task DeleteHallAsync(Guid hallId);

        // Sessions
        Task<MovieSessionDetailsDTO?> GetSessionDetailsAsync(Guid sessionId);
        Task<MovieSessionListDTO> CreateSessionAsync(MovieSessionCreateDTO dto);
        Task UpdateSessionAsync(MovieSessionUpdateDTO dto);
        Task DeleteSessionAsync(Guid sessionId);
    }
}