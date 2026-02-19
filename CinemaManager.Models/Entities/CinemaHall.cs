using CinemaManager.Models.Enums;

namespace CinemaManager.Models.Entities
{
    /// <summary>
    /// Сутність першого рівня, що представляє кінозал.
    /// Містить лише базові дані для зберігання.
    /// </summary>
    public class CinemaHall
    {
        public Guid Id { get; } // Унікальний ідентифікатор без можливості зміни
        public string Name { get; set; }
        public int SeatsCount { get; set; }
        public HallType HallType { get; set; } // Тип кінозалу (2D, 3D, IMAX)

        public CinemaHall(Guid id, string name, int seatsCount, HallType hallType)
        {
            Id = id;
            Name = name;
            SeatsCount = seatsCount;
            HallType = hallType;
        }
    }
}
