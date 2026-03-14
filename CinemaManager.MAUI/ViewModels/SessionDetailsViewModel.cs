using CommunityToolkit.Mvvm.ComponentModel;
using CinemaManager.Services;
using CinemaManager.Services.DTO;

namespace CinemaManager.MAUI.ViewModels
{
    // "Ловимо" параметр SessionId, який ми передали з другої сторінки
    [QueryProperty(nameof(SessionId), "SessionId")]
    public partial class SessionDetailsViewModel : ObservableObject
    {
        private readonly ICinemaService _cinemaService;

        [ObservableProperty]
        private Guid sessionId;

        [ObservableProperty]
        private MovieSessionDetailsDTO sessionDetails;

        public SessionDetailsViewModel(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        // Автоматично спрацьовує, коли MAUI записує сюди отриманий ID
        partial void OnSessionIdChanged(Guid value)
        {
            // Отримуємо всі деталі сеансу з бази через сервіс
            SessionDetails = _cinemaService.GetSessionDetails(value);
        }
    }
}