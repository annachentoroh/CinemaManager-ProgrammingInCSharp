using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CinemaManager.Services;
using CinemaManager.Services.DTO;

namespace CinemaManager.MAUI.ViewModels
{
    // Атрибут QueryProperty "ловить" параметр HallId, який ми передали з головної сторінки
    [QueryProperty(nameof(HallId), "HallId")]
    public partial class HallDetailsViewModel : ObservableObject
    {
        private readonly ICinemaService _cinemaService;

        [ObservableProperty]
        private Guid hallId;

        [ObservableProperty]
        private CinemaHallDetailsDTO hallDetails;

        [ObservableProperty]
        private int totalDuration;

        [ObservableProperty]
        private MovieSessionListDTO selectedSession;

        public HallDetailsViewModel(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        // Цей метод автоматично викликається, коли MAUI записує сюди HallId
        partial void OnHallIdChanged(Guid value)
        {
            // Завантажуємо деталі залу і його сеанси
            HallDetails = _cinemaService.GetHallDetails(value);
            // Рахуємо суму хвилин усіх сеансів у цьому залі
            if (HallDetails != null && HallDetails.Sessions != null)
            {
                TotalDuration = HallDetails.Sessions.Sum(s => s.DurationMinutes);
            }
        }

        [RelayCommand]
        private async Task SessionSelectedAsync()
        {
            if (SelectedSession == null) return;

            var parameters = new Dictionary<string, object>
            {
                { "SessionId", SelectedSession.Id }
            };

            // Перехід на третю сторінку
            await Shell.Current.GoToAsync(nameof(SessionDetailsPage), parameters);
            SelectedSession = null;
        }
    }
}