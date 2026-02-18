using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using CinemaManager.UI;
using CinemaManager.DB;
using CinemaManager.Models.Entities;

namespace CinemaManager.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            CinemaRepo storageService = new CinemaRepo();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЕДЖЕР КІНОСЕАНСІВ ===");

                //(Entities)
                List<CinemaHall> hallEntities = storageService.GetAllHalls();

                List<CinemaHallUI> uiHalls = hallEntities
                                             .Select(h => new CinemaHallUI(h))
                                             .ToList();

                Console.WriteLine("Список кінозалів:");
                for (int i = 0; i < uiHalls.Count; i++)
                {
                    var uiHall = uiHalls[i];

                    var originalEntity = hallEntities[i];
                    storageService.LoadSessionsForHall(originalEntity);

                   
                    //Синх Entity -> UI
                    UpdateUISessions(uiHall, originalEntity);

                    Console.WriteLine($"{i + 1}. {uiHall}");
                }

                Console.WriteLine("0. Вихід");
                Console.Write("\nОберіть номер залу для перегляду деталей: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) break;

                    if (choice > 0 && choice <= uiHalls.Count)
                    {
                        var selectedUIHall = uiHalls[choice - 1];

                        // Передаємо його у метод відображення
                        ShowHallDetails(selectedUIHall);
                    }
                    else
                    {
                        Console.WriteLine("Невірний номер. Натисніть Enter.");
                        Console.ReadLine();
                    }
                }
            }
        }

        // Цей метод допомагає перекласти сеанси з Entity в UI
        static void UpdateUISessions(CinemaHallUI uiModel, CinemaHall entityModel)
        {
            uiModel.Sessions.Clear();
            foreach (var sessionEntity in entityModel.MovieSessions)
            {
                // Обгортаємо сеанс-entity в сеанс-UI і додаємо в список UI-залу
                uiModel.Sessions.Add(new MovieSessionUI(sessionEntity));
            }
        }

        // Тепер цей метод приймає CinemaHallUI, а не CinemaHall
        static void ShowHallDetails(CinemaHallUI hall)
        {
            while (true)
            {
                Console.Clear();

                // Тут спрацює ToString() з CinemaHallUI
                Console.WriteLine($"--- ДЕТАЛІ ЗАЛУ: {hall.Name} ---");
                Console.WriteLine(hall.ToString());
                Console.WriteLine("-----------------------------");

                if (hall.Sessions.Count == 0)
                {
                    Console.WriteLine("Сеанси відсутні.");
                }
                else
                {
                    Console.WriteLine("КІНОСЕАНСИ:");
                    for (int i = 0; i < hall.Sessions.Count; i++)
                    {
                        // Тут спрацює ToString() з MovieSessionUI
                        Console.WriteLine($"{i + 1}. {hall.Sessions[i]}");
                    }
                }

                Console.WriteLine("\nНатисніть Enter, щоб повернутися до списку залів...");
                Console.ReadLine();
                break;
            }
        }
    }
}