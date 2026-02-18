using System;
using System.Collections.Generic;
using System.Linq;
using CinemaManager.Models.Entities;
using CinemaManager.Models.Enums;

namespace CinemaManager.UI
{
    public class CinemaHallUI
    {
        private readonly CinemaHall _entity;
        public List<MovieSessionUI> Sessions { get; set; } = new List<MovieSessionUI>();

        public CinemaHallUI(CinemaHall entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        public Guid Id => _entity.Id;
        public string Name
        {
            get => _entity.Name;
            set => _entity.Name = value;
        }
        public int SeatsCount => _entity.SeatsCount;
        public HallType HallType => _entity.HallType;
        public double TotalDurationMinutes => Sessions.Sum(s => s.DurationMinutes);

        public override string ToString()
        {
            return $"Зал: {Name} [{HallType}] | Місць: {SeatsCount} | Сеансів: {Sessions.Count} | Загальна тривалість: {TotalDurationMinutes} год";
        }
    }
}
