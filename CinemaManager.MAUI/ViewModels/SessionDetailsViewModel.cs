using CinemaManager.Services;
using CinemaManager.Services.DTO;

namespace CinemaManager.MAUI.ViewModels
{
    public class SessionDetailsViewModel : BindableObject, IQueryAttributable
    {
        private readonly ICinemaService _cinemaService;

        private MovieSessionDetailsDTO _sessionDetails;
        public MovieSessionDetailsDTO SessionDetails
        {
            get => _sessionDetails;
            set { _sessionDetails = value; OnPropertyChanged(); }
        }

        public SessionDetailsViewModel(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("SessionId", out var sessionIdObj) && sessionIdObj is Guid id)
            {
                LoadSessionData(id);
            }
        }

        private void LoadSessionData(Guid sessionId)
        {
            SessionDetails = _cinemaService.GetSessionDetails(sessionId);
        }
    }
}