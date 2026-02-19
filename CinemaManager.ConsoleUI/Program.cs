namespace CinemaManager.ConsoleUI
{
    /// <summary>
    /// Точка входу в консольний застосунок.
    /// Відповідає за взаємодію з користувачем.
    /// </summary>
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
                Console.WriteLine("МЕНЕДЖЕР КІНОСЕАНСІВ\n");

                // Отримуємо сутності першого рівня
                List<CinemaHall> hallEntities = storageService.GetAllHalls();

                // Формуємо UI-моделі
                List<CinemaHallUI> uiHalls = hallEntities
                    .Select(h => new CinemaHallUI(h))
                    .ToList();

                Console.WriteLine("Список кінозалів:");

                for (int i = 0; i < uiHalls.Count; i++)
                {
                    var uiHall = uiHalls[i];

                    // Завантажуємо сеанси через Repository
                    var sessionEntities = storageService
                        .GetSessionsByHallId(uiHall.Id);

                    uiHall.Sessions = sessionEntities
                        .Select(s => new MovieSessionUI(s))
                        .ToList();

                    Console.WriteLine($"{i + 1}. {uiHall}");
                }

                Console.WriteLine("0. Вихід");
                Console.Write("\nОберіть номер залу для перегляду деталей: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                    continue;

                if (choice == 0)
                    break;

                if (choice > 0 && choice <= uiHalls.Count)
                {
                    ShowHallDetails(uiHalls[choice - 1]);
                }
                else
                {
                    Console.WriteLine("Невірний номер. Натисніть Enter.");
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Відображає детальну інформацію про вибраний кінозал
        /// та список його кіносеансів.
        /// </summary>
        static void ShowHallDetails(CinemaHallUI hall)
        {
            Console.Clear();

            Console.WriteLine($"ДЕТАЛІ ЗАЛУ: {hall.Name} ");
            Console.WriteLine(hall);

            if (hall.Sessions.Count == 0)
            {
                Console.WriteLine("Сеанси відсутні.");
            }
            else
            {
                Console.WriteLine("КІНОСЕАНСИ:");

                for (int i = 0; i < hall.Sessions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {hall.Sessions[i]}");
                }
            }

            Console.WriteLine("\nНатисніть Enter, щоб повернутися до списку залів...");
            Console.ReadLine();
        }
    }
}
