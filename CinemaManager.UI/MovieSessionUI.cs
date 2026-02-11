using System;
using CinemaManager.Models.Entities;
using CinemaManager.Models.Enums;

namespace CinemaManager.UI
{
    public class MovieSessionUI
    {
        private readonly MovieSession _entity;

        public MovieSessionUI(MovieSession entity)
        {
            _entity = entity;
        }

        public Guid Id => _entity.Id;
        public Guid CinemaHallId => _entity.CinemaHallId;
        public string MovieTitle
        {
            get => _entity.MovieTitle;
            set => _entity.MovieTitle = value;
        }
        public Genre Genre => _entity.Genre;
        public DateTime StartTime => _entity.StartTime;
        public int DurationMinutes => _entity.DurationMinutes;
        public DateTime EndTime => StartTime.AddMinutes(DurationMinutes);

        public override string ToString()
        {
            return $"{MovieTitle} ({Genre}) | {StartTime:HH:mm} - {EndTime:HH:mm}";
        }
    }
}
