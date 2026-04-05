using CinemaManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.DB
{
    public class CinemaRepo : ICinemaRepo
    {
        private readonly DbContextFactory _factory;

        public CinemaRepo(DbContextFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<CinemaHall>> GetAllHallsAsync()
        {
            await using var db = _factory.Create();
            return await db.CinemaHalls.ToListAsync();
        }

        public async Task<CinemaHall?> GetHallByIdAsync(Guid hallId)
        {
            await using var db = _factory.Create();
            return await db.CinemaHalls
                .Include(h => h.Sessions)
                .FirstOrDefaultAsync(h => h.Id == hallId);
        }

        public async Task<List<MovieSession>> GetSessionsByHallIdAsync(Guid hallId)
        {
            await using var db = _factory.Create();
            return await db.MovieSessions
                .Where(s => s.CinemaHallId == hallId)
                .ToListAsync();
        }

        public async Task<MovieSession?> GetSessionByIdAsync(Guid sessionId)
        {
            await using var db = _factory.Create();
            return await db.MovieSessions.FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public async Task<CinemaHall> AddHallAsync(CinemaHall hall)
        {
            await using var db = _factory.Create();
            db.CinemaHalls.Add(hall);
            await db.SaveChangesAsync();
            return hall;
        }

        public async Task<MovieSession> AddSessionAsync(MovieSession session)
        {
            await using var db = _factory.Create();
            db.MovieSessions.Add(session);
            await db.SaveChangesAsync();
            return session;
        }

        public async Task UpdateHallAsync(CinemaHall hall)
        {
            await using var db = _factory.Create();
            db.CinemaHalls.Update(hall);
            await db.SaveChangesAsync();
        }

        public async Task UpdateSessionAsync(MovieSession session)
        {
            await using var db = _factory.Create();
            db.MovieSessions.Update(session);
            await db.SaveChangesAsync();
        }

        public async Task DeleteHallAsync(Guid hallId)
        {
            await using var db = _factory.Create();
            var hall = await db.CinemaHalls
                .Include(h => h.Sessions)
                .FirstOrDefaultAsync(h => h.Id == hallId);
            if (hall != null)
            {
                db.CinemaHalls.Remove(hall); // Cascade видалить сеанси
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteSessionAsync(Guid sessionId)
        {
            await using var db = _factory.Create();
            var session = await db.MovieSessions.FindAsync(sessionId);
            if (session != null)
            {
                db.MovieSessions.Remove(session);
                await db.SaveChangesAsync();
            }
        }
    }
}