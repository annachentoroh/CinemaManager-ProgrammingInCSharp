namespace CinemaManager.Services.DTO
{
    public class CinemaHallListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HallType { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
    }

    public class CinemaHallDetailsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public string HallType { get; set; } = string.Empty;
        public List<MovieSessionListDTO> Sessions { get; set; } = new();
    }

    public class CinemaHallCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public string HallType { get; set; } = string.Empty;
    }

    public class CinemaHallUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalSeats { get; set; }
        public string HallType { get; set; } = string.Empty;
    }
}