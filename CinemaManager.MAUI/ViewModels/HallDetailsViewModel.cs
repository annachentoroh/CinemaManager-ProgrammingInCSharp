using CinemaManager.Services;
using CinemaManager.Services.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaManager.MAUI.ViewModels
{
    public class HallDetailsViewModel : BindableObject, IQueryAttributable
    {
        private readonly ICinemaService _cinemaService;

        public Guid HallId { get; set; }

        private string _hallName;
        public string HallName
        {
            get => _hallName;
            set { _hallName = value; OnPropertyChanged(); }
        }

        private string _hallDescription;
        public string HallDescription
        {
            get => _hallDescription;
            set { _hallDescription = value; OnPropertyChanged(); }
        }

        private int _totalSeats;
        public int TotalSeats
        {
            get => _totalSeats;
            set { _totalSeats = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MovieSessionListDTO> Sessions { get; set; } = new ObservableCollection<MovieSessionListDTO>();

        private MovieSessionListDTO _selectedSession;
        public MovieSessionListDTO SelectedSession
        {
            get => _selectedSession;
            set { _selectedSession = value; OnPropertyChanged(); }
        }

        public ICommand SessionSelectedCommand { get; }

        public HallDetailsViewModel(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
            SessionSelectedCommand = new Command(GoToSessionDetails);
        }

        // Цей метод ловить словник з попередньої сторінки
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("HallId", out var hallIdObj) && hallIdObj is Guid id)
            {
                HallId = id;
                LoadHallData(); // Завантажуємо дані ТІЛЬКИ коли точно маємо Guid
            }
        }

        private void LoadHallData()
        {
            var hallDetails = _cinemaService.GetHallDetails(HallId);

            if (hallDetails != null)
            {
                HallName = hallDetails.Name;
                HallDescription = hallDetails.Description;
                TotalSeats = hallDetails.TotalSeats;

                Sessions.Clear();
                foreach (var session in hallDetails.Sessions)
                {
                    Sessions.Add(session);
                }
            }
        }

        private void GoToSessionDetails()
        {
            if (SelectedSession == null) return;

            var parameters = new Dictionary<string, object>
            {
                { "SessionId", SelectedSession.Id }
            };

            Shell.Current.GoToAsync($"{nameof(SessionDetailsPage)}", parameters);

            SelectedSession = null;
        }
    }
}