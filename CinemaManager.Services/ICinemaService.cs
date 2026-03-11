using CinemaManager.Services.DTO;

namespace CinemaManager.Services
{
    public interface ICinemaService
    {
        // Отримати всі зали для головної сторінки
        IEnumerable<CinemaHallListDTO> GetAllHalls();

        // Отримати деталі залу та його сеанси для другої сторінки
        CinemaHallDetailsDTO GetHallDetails(Guid hallId);

        // Отримати деталі конкретного сеансу для третьої сторінки
        MovieSessionDetailsDTO GetSessionDetails(Guid sessionId);
    }
}