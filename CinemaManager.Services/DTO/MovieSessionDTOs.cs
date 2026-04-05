namespace CinemaManager.Services.DTO
{
    public class MovieSessionListDTO
    {
        public Guid Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }
        public int ReleaseYear { get; set; }
    }

    public class MovieSessionDetailsDTO
    {
        public Guid Id { get; set; }
        public Guid CinemaHallId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class MovieSessionCreateDTO
    {
        public Guid CinemaHallId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }
    }

    public class MovieSessionUpdateDTO
    {
        public Guid Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }
    }
}