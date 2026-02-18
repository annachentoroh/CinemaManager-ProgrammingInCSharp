using CinemaManager.Models.Enums;

namespace CinemaManager.Models.Entities
{
    public class MovieSession
    {
        public Guid Id { get; }
        public Guid CinemaHallId { get; }
        public string MovieTitle { get; set; }
        public Genre Genre { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime StartTime { get; set; }
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

        // Конструктор для мапінгу
        public MovieSession(dynamic data)
        {
            Id = data.Id;
            CinemaHallId = data.CinemaHallId;
            MovieTitle = data.MovieTitle;
            Genre = data.Genre;
            ReleaseYear = data.ReleaseYear;
            StartTime = data.StartTime;
            DurationMinutes = data.DurationMinutes;
        }

        public MovieSession() { }
    }
}
