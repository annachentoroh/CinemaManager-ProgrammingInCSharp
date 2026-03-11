namespace CinemaManager.Services.DTO
{
    // Модель для списку
    public class CinemaHallListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    // Модель для деталей
    public class CinemaHallDetailsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalSeats { get; set; }

        // Список сеансів у цьому залі
        public List<MovieSessionListDTO> Sessions { get; set; } = new();
    }
}