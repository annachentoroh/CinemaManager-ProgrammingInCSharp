using CinemaManager.Models.Entities;

namespace CinemaManager.DB
{
    /// <summary>
    /// Клас, який відповідає за взаємодію зі сховищем.
    /// Надає доступ до сутностей зберігання.
    /// </summary>
    public class CinemaRepo
    {
        public List<CinemaHall> GetAllHalls() // Отримання всіх залів (сутностей першого рівня)
        {
            return Database.CinemaHalls.ToList();
        }

        // Отримання сеансів, пов'язаних з конкретним залом за його ID
        public List<MovieSession> GetSessionsByHallId(Guid hallId)
        {
            return Database.MovieSessions
                .Where(s => s.CinemaHallId == hallId)
                .ToList();
        }
    }
}
