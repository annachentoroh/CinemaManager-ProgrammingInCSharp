using System;
using System.Collections.Generic;
using System.Linq;
using CinemaManager.Models.Entities;
//using CinemaManager.UI;

namespace CinemaManager.DB
{
    public class CinemaRepo
    {
        public List<CinemaHall> GetAllHalls()
        {
            // Використовуємо .Select для трансформації даних у моделі
            return Database.CinemaHalls
                .Select(hData => new CinemaHall(hData))
                .ToList();
        }

        public void LoadSessionsForHall(CinemaHall hallModel)
        {
            if (hallModel == null) return;

            // Отримуємо дані, фільтруємо та перетворюємо в моделі одним ланцюжком
            var sessions = Database.MovieSessions
                .Where(s => s.CinemaHallId == hallModel.Id)
                .Select(sData => new MovieSession(sData))
                .ToList();

            hallModel.MovieSessions = sessions;
        }
    }
}
