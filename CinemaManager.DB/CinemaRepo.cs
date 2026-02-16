using CinemaManager.Models.Entities;
using CinemaManager.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaManager.DB
{
    public class CinemaRepo
    {
        public List<CinemaHallUI> GetAllHalls()
        {
            return Database.CinemaHalls
                .Select(h => new CinemaHallUI(h))
                .ToList();
        }
        public List<MovieSessionUI> GetSessionsByHallId(Guid hallId)
        {
            return Database.MovieSessions
                .Where(s => s.CinemaHallId == hallId)
                .Select(s => new MovieSessionUI(s))
                .ToList();
        }
    }
}