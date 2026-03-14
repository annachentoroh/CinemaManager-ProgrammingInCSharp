namespace CinemaManager.Services.DTO
{
    // Модель для списку сеансів
    public class MovieSessionListDTO
    {
        public Guid Id { get; set; }
        public string MovieTitle { get; set; }
        public DateTime StartTime { get; set; }
        public string Genre { get; set; } // Додали це
        public int DurationMinutes { get; set; } // Знадобиться для підрахунку
    }

    // Модель для детальної інформації про сеанс
    public class MovieSessionDetailsDTO
    {
        public Guid Id { get; set; }
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public decimal TicketPrice { get; set; }
    }
}