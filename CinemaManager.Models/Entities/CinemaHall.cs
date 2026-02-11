using CinemaManager.Models.Enums;

namespace CinemaManager.Models.Entities
{
    public class CinemaHall
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int SeatsCount { get; set; }
        public HallType HallType { get; set; }

        public CinemaHall(Guid id, string name, int seatsCount, HallType hallType)
        {
            Id = id;
            Name = name;
            SeatsCount = seatsCount;
            HallType = hallType;
        }
    }
}
