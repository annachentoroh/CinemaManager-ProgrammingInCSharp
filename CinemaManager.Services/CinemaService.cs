using CinemaManager.DB;
using CinemaManager.Models.Entities;
using CinemaManager.Models.Enums;
using CinemaManager.Services.DTO;

namespace CinemaManager.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepo _repository;

        public CinemaService(ICinemaRepo repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<CinemaHallListDTO>> GetAllHallsAsync()
        {
            var halls = await _repository.GetAllHallsAsync();
            return halls.Select(h => new CinemaHallListDTO
            {
                Id = h.Id,
                Name = h.Name,
                HallType = h.HallType.ToString(),
                TotalSeats = h.SeatsCount
            });
        }

        public async Task<CinemaHallDetailsDTO?> GetHallDetailsAsync(Guid hallId)
        {
            var hall = await _repository.GetHallByIdAsync(hallId);
            if (hall == null) return null;

            var sessions = await _repository.GetSessionsByHallIdAsync(hallId);

            return new CinemaHallDetailsDTO
            {
                Id = hall.Id,
                Name = hall.Name,
                TotalSeats = hall.SeatsCount,
                HallType = hall.HallType.ToString(),
                Sessions = sessions.Select(s => new MovieSessionListDTO
                {
                    Id = s.Id,
                    MovieTitle = s.MovieTitle,
                    StartTime = s.StartTime,
                    Genre = s.Genre.ToString(),
                    DurationMinutes = s.DurationMinutes,
                    ReleaseYear = s.ReleaseYear
                }).ToList()
            };
        }

        public async Task<CinemaHallListDTO> CreateHallAsync(CinemaHallCreateDTO dto)
        {
            var hallType = Enum.Parse<HallType>(dto.HallType);
            var hall = new CinemaHall(Guid.NewGuid(), dto.Name, dto.TotalSeats, hallType);
            var saved = await _repository.AddHallAsync(hall);
            return new CinemaHallListDTO
            {
                Id = saved.Id,
                Name = saved.Name,
                HallType = saved.HallType.ToString(),
                TotalSeats = saved.SeatsCount
            };
        }

        public async Task UpdateHallAsync(CinemaHallUpdateDTO dto)
        {
            var hall = await _repository.GetHallByIdAsync(dto.Id);
            if (hall == null) return;

            hall.Name = dto.Name;
            hall.SeatsCount = dto.TotalSeats;
            hall.HallType = Enum.Parse<HallType>(dto.HallType);
            await _repository.UpdateHallAsync(hall);
        }

        public async Task DeleteHallAsync(Guid hallId)
        {
            await _repository.DeleteHallAsync(hallId);
        }

        
        public async Task<MovieSessionDetailsDTO?> GetSessionDetailsAsync(Guid sessionId)
        {
            var session = await _repository.GetSessionByIdAsync(sessionId);
            if (session == null) return null;

            return new MovieSessionDetailsDTO
            {
                Id = session.Id,
                CinemaHallId = session.CinemaHallId,
                MovieTitle = session.MovieTitle,
                Genre = session.Genre.ToString(),
                ReleaseYear = session.ReleaseYear,
                DurationMinutes = session.DurationMinutes,
                StartTime = session.StartTime,
            };
        }

        public async Task<MovieSessionListDTO> CreateSessionAsync(MovieSessionCreateDTO dto)
        {
            var genre = Enum.Parse<Genre>(dto.Genre);
            var session = new MovieSession(
                Guid.NewGuid(), dto.CinemaHallId, dto.MovieTitle,
                genre, dto.ReleaseYear, dto.StartTime, dto.DurationMinutes);
            var saved = await _repository.AddSessionAsync(session);
            return new MovieSessionListDTO
            {
                Id = saved.Id,
                MovieTitle = saved.MovieTitle,
                Genre = saved.Genre.ToString(),
                StartTime = saved.StartTime,
                DurationMinutes = saved.DurationMinutes,
                ReleaseYear = saved.ReleaseYear
            };
        }

        public async Task UpdateSessionAsync(MovieSessionUpdateDTO dto)
        {
            var session = await _repository.GetSessionByIdAsync(dto.Id);
            if (session == null) return;

            session.MovieTitle = dto.MovieTitle;
            session.Genre = Enum.Parse<Genre>(dto.Genre);
            session.ReleaseYear = dto.ReleaseYear;
            session.StartTime = dto.StartTime;
            session.DurationMinutes = dto.DurationMinutes;
            await _repository.UpdateSessionAsync(session);
        }

        public async Task DeleteSessionAsync(Guid sessionId)
        {
            await _repository.DeleteSessionAsync(sessionId);
        }
    }
}