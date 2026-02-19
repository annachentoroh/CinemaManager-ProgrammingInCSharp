using CinemaManager.Models.Enums;

namespace CinemaManager.Models.Entities
{
    /// <summary>
    /// Сутність другого рівня.
    /// Зберігає дані про кіносеанс.
    /// <summary>
    public class MovieSession
    {
        /// <summary>
        /// Унікальний ідентифікатор кіносеансу.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Ідентифікатор кінозалу, якому належить сеанс.
        /// </summary>
        public Guid CinemaHallId { get; }

        /// <summary>
        /// Назва фільму.
        /// </summary>
        public string MovieTitle { get; set; }

        /// <summary>
        /// Жанр фільму.
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        /// Рік випуску фільму.
        /// </summary>
        public int ReleaseYear { get; set; }

        /// <summary>
        /// Час початку показу.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Тривалість фільму у хвилинах.
        /// </summary>
        public int DurationMinutes { get; set; }

        public MovieSession(
            Guid id,
            Guid cinemaHallId,
            string movieTitle,
            Genre genre,
            int releaseYear,
            DateTime startTime,
            int durationMinutes)
        {
            Id = id;
            CinemaHallId = cinemaHallId;
            MovieTitle = movieTitle;
            Genre = genre;
            ReleaseYear = releaseYear;
            StartTime = startTime;
            DurationMinutes = durationMinutes;
        }
    }
}
