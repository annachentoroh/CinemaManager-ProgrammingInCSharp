using CinemaManager.Services;
using CinemaManager.Services.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaManager.MAUI.ViewModels
{
    [QueryProperty(nameof(HallId), "hallId")]
    public class HallDetailsViewModel : BaseViewModel
    {
        private readonly ICinemaService _service;

        private string? _hallId;
        public string? HallId
        {
            get => _hallId;
            set { _hallId = value; OnPropertyChanged(); }
        }

        private CinemaHallDetailsDTO? _hallDetails;
        public CinemaHallDetailsDTO? HallDetails
        {
            get => _hallDetails;
            set => SetProperty(ref _hallDetails, value);
        }

        private List<MovieSessionListDTO> _allSessions = new();

        private ObservableCollection<MovieSessionListDTO> _sessions = new();
        public ObservableCollection<MovieSessionListDTO> Sessions
        {
            get => _sessions;
            set => SetProperty(ref _sessions, value);
        }

        private MovieSessionListDTO? _selectedSession;
        public MovieSessionListDTO? SelectedSession
        {
            get => _selectedSession;
            set => SetProperty(ref _selectedSession, value);
        }

        // Загальна тривалість
        public int TotalDuration => _allSessions.Sum(s => s.DurationMinutes);

        // Пошук
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set { SetProperty(ref _searchText, value); ApplyFilter(); }
        }

        // Сортування
        private string _sortOption = "Час ↑";
        public string SortOption
        {
            get => _sortOption;
            set { SetProperty(ref _sortOption, value); ApplyFilter(); }
        }

        public List<string> SortOptions { get; } = new()
        {
            "Час ↑", "Час ↓", "Назва ↑", "Назва ↓", "Тривалість ↑", "Тривалість ↓"
        };

        // Команди
        public ICommand LoadCommand { get; }
        public ICommand SessionSelectedCommand { get; }
        public ICommand AddSessionCommand { get; }
        public ICommand DeleteSessionCommand { get; }
        public ICommand EditSessionCommand { get; }
        public ICommand EditHallCommand { get; }

        public HallDetailsViewModel(ICinemaService service)
        {
            _service = service;
            LoadCommand = new Command(async () => await LoadAsync());
            SessionSelectedCommand = new Command(async () => await NavigateToSessionAsync());
            AddSessionCommand = new Command(async () => await AddSessionAsync());
            DeleteSessionCommand = new Command<MovieSessionListDTO>(async (s) => await DeleteSessionAsync(s));
            EditSessionCommand = new Command<MovieSessionListDTO>(async (s) => await EditSessionAsync(s));
            EditHallCommand = new Command(async () => await EditHallAsync());
        }

        public async Task LoadAsync()
        {
            if (string.IsNullOrEmpty(HallId)) return;

            await ExecuteBusyAsync(async () =>
            {
                var details = await _service.GetHallDetailsAsync(Guid.Parse(HallId));
                HallDetails = details;
                _allSessions = details?.Sessions ?? new();
                OnPropertyChanged(nameof(TotalDuration));
                ApplyFilter();
            });
        }

        private void ApplyFilter()
        {
            var filtered = _allSessions
                .Where(s => string.IsNullOrWhiteSpace(SearchText) ||
                            s.MovieTitle.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                            s.Genre.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            filtered = SortOption switch
            {
                "Час ↑" => filtered.OrderBy(s => s.StartTime).ToList(),
                "Час ↓" => filtered.OrderByDescending(s => s.StartTime).ToList(),
                "Назва ↑" => filtered.OrderBy(s => s.MovieTitle).ToList(),
                "Назва ↓" => filtered.OrderByDescending(s => s.MovieTitle).ToList(),
                "Тривалість ↑" => filtered.OrderBy(s => s.DurationMinutes).ToList(),
                "Тривалість ↓" => filtered.OrderByDescending(s => s.DurationMinutes).ToList(),
                _ => filtered
            };

            Sessions = new ObservableCollection<MovieSessionListDTO>(filtered);
        }

        private async Task NavigateToSessionAsync()
        {
            if (SelectedSession == null) return;
            var id = SelectedSession.Id;
            SelectedSession = null;
            await Shell.Current.GoToAsync($"{nameof(SessionDetailsPage)}?sessionId={id}");
        }

        private async Task AddSessionAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(SessionEditPage)}?hallId={HallId}");
        }

        private async Task DeleteSessionAsync(MovieSessionListDTO session)
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Видалення", $"Видалити сеанс '{session.MovieTitle}'?", "Так", "Ні");
            if (!confirm) return;

            await ExecuteBusyAsync(async () =>
            {
                await _service.DeleteSessionAsync(session.Id);
                await LoadAsync();
            }, "Видалення...");
        }

        private async Task EditSessionAsync(MovieSessionListDTO session)
        {
            await Shell.Current.GoToAsync($"{nameof(SessionEditPage)}?sessionId={session.Id}&hallId={HallId}");
        }

        private async Task EditHallAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(HallEditPage)}?hallId={HallId}");
        }
    }
}