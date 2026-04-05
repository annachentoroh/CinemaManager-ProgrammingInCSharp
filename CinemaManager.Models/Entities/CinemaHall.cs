using CinemaManager.Models.Enums;

namespace CinemaManager.Models.Entities
{
    public class CinemaHall
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SeatsCount { get; set; }
        public HallType HallType { get; set; }

        public List<MovieSession> Sessions { get; set; } = new();

        // Потрібен для EF Core
        protected CinemaHall() { }

        public CinemaHall(Guid id, string name, int seatsCount, HallType hallType)
        {
            Id = id;
            Name = name;
            SeatsCount = seatsCount;
            HallType = hallType;
        }
    }
}