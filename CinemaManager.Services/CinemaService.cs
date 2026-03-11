using CinemaManager.Services.DTO;
using CinemaManager.DB;
using CinemaManager.Models.Entities;

namespace CinemaManager.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepo _repository;

        // Впровадження репозиторію через конструктор
        public CinemaService(ICinemaRepo repository)
        {
            _repository = repository;
        }

        public IEnumerable<CinemaHallListDTO> GetAllHalls()
        {
            var halls = _repository.GetAllHalls();
            return halls.Select(h => new CinemaHallListDTO
            {
                Id = h.Id,
                Name = h.Name
            });
        }

        public CinemaHallDetailsDTO GetHallDetails(Guid hallId)
        {
            var hall = _repository.GetHallById(hallId);
            if (hall == null) return null;

            return new CinemaHallDetailsDTO
            {
                Id = hall.Id,
                Name = hall.Name,
                TotalSeats = hall.SeatsCount,
                Sessions = hall.Sessions.Select(s => new MovieSessionListDTO
                {
                    Id = s.Id,
                    MovieTitle = s.MovieTitle,
                    StartTime = s.StartTime
                }).ToList()
            };
        }

        public MovieSessionDetailsDTO GetSessionDetails(Guid sessionId)
        {
            var session = _repository.GetSessionById(sessionId);
            if (session == null) return null;

            return new MovieSessionDetailsDTO
            {
                Id = session.Id,
                MovieTitle = session.MovieTitle,
                Genre = session.Genre.ToString(),
                DurationMinutes = session.DurationMinutes,
                StartTime = session.StartTime,
            };
        }
    }
}