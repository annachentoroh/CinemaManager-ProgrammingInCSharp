using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CinemaManager.Services;
using CinemaManager.Services.DTO;
using System.Collections.ObjectModel;

namespace CinemaManager.MAUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ICinemaService _cinemaService;

        // Тепер використовуємо CinemaHallDetailsDTO, бо там є TotalSeats
        [ObservableProperty]
        private ObservableCollection<CinemaHallDetailsDTO> halls;

        [ObservableProperty]
        private CinemaHallDetailsDTO selectedHall;

        public MainViewModel(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
            LoadHalls();
        }

        private void LoadHalls()
        {
            var basicHalls = _cinemaService.GetAllHalls();
            var detailedHalls = new ObservableCollection<CinemaHallDetailsDTO>();

            // Завантажуємо деталі для кожного залу, щоб відобразити місця
            foreach (var basicHall in basicHalls)
            {
                var details = _cinemaService.GetHallDetails(basicHall.Id);
                if (details != null)
                {
                    detailedHalls.Add(details);
                }
            }
            Halls = detailedHalls;
        }

        [RelayCommand]
        private async Task HallSelectedAsync()
        {
            if (SelectedHall == null) return;

            var parameters = new Dictionary<string, object>
            {
                { "HallId", SelectedHall.Id }
            };

            await Shell.Current.GoToAsync(nameof(HallDetailsPage), parameters);
            SelectedHall = null;
        }
    }
}