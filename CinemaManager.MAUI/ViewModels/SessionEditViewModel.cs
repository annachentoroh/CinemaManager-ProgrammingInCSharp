using CinemaManager.Models.Enums;
using CinemaManager.Services;
using CinemaManager.Services.DTO;
using System.Windows.Input;

namespace CinemaManager.MAUI.ViewModels
{
    [QueryProperty(nameof(SessionId), "sessionId")]
    [QueryProperty(nameof(HallId), "hallId")]
    public class SessionEditViewModel : BaseViewModel
    {
        private readonly ICinemaService _service;

        private string? _sessionId;
        public string? SessionId
        {
            get => _sessionId;
            set { _sessionId = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsEditMode)); OnPropertyChanged(nameof(PageTitle)); }
        }

        private string? _hallId;
        public string? HallId
        {
            get => _hallId;
            set { _hallId = value; OnPropertyChanged(); }
        }

        public bool IsEditMode => !string.IsNullOrEmpty(SessionId);
        public string PageTitle => IsEditMode ? "Редагувати сеанс" : "Новий сеанс";

        // Форма
        private string _movieTitle = string.Empty;
        public string MovieTitle
        {
            get => _movieTitle;
            set => SetProperty(ref _movieTitle, value);
        }

        private string _selectedGenre = "Drama";
        public string SelectedGenre
        {
            get => _selectedGenre;
            set => SetProperty(ref _selectedGenre, value);
        }

        private int _releaseYear = DateTime.Today.Year;
        public int ReleaseYear
        {
            get => _releaseYear;
            set => SetProperty(ref _releaseYear, value);
        }

        private DateTime _startTime = DateTime.Today.AddHours(10);
        public DateTime StartTime
        {
            get => _startTime;
            set => SetProperty(ref _startTime, value);
        }

        private TimeSpan _startTimeOfDay = TimeSpan.FromHours(10);
        public TimeSpan StartTimeOfDay
        {
            get => _startTimeOfDay;
            set { SetProperty(ref _startTimeOfDay, value); StartTime = StartDate.Date + value; }
        }

        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate
        {
            get => _startDate;
            set { SetProperty(ref _startDate, value); StartTime = value.Date + StartTimeOfDay; }
        }

        private int _durationMinutes = 90;
        public int DurationMinutes
        {
            get => _durationMinutes;
            set => SetProperty(ref _durationMinutes, value);
        }

        public List<string> Genres { get; } = Enum.GetNames<Genre>().ToList();

        public ICommand LoadCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public SessionEditViewModel(ICinemaService service)
        {
            _service = service;
            LoadCommand = new Command(async () => await LoadAsync());
            SaveCommand = new Command(async () => await SaveAsync());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        public async Task LoadAsync()
        {
            if (!IsEditMode) return;
            await ExecuteBusyAsync(async () =>
            {
                var details = await _service.GetSessionDetailsAsync(Guid.Parse(SessionId!));
                if (details == null) return;
                MovieTitle = details.MovieTitle;
                SelectedGenre = details.Genre;
                ReleaseYear = details.ReleaseYear;
                DurationMinutes = details.DurationMinutes;
                StartDate = details.StartTime.Date;
                StartTimeOfDay = details.StartTime.TimeOfDay;
            });
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(MovieTitle))
            {
                await Shell.Current.DisplayAlert("Помилка", "Введіть назву фільму", "OK");
                return;
            }

            await ExecuteBusyAsync(async () =>
            {
                if (IsEditMode)
                {
                    await _service.UpdateSessionAsync(new MovieSessionUpdateDTO
                    {
                        Id = Guid.Parse(SessionId!),
                        MovieTitle = MovieTitle,
                        Genre = SelectedGenre,
                        ReleaseYear = ReleaseYear,
                        StartTime = StartTime,
                        DurationMinutes = DurationMinutes
                    });
                }
                else
                {
                    await _service.CreateSessionAsync(new MovieSessionCreateDTO
                    {
                        CinemaHallId = Guid.Parse(HallId!),
                        MovieTitle = MovieTitle,
                        Genre = SelectedGenre,
                        ReleaseYear = ReleaseYear,
                        StartTime = StartTime,
                        DurationMinutes = DurationMinutes
                    });
                }
                await Shell.Current.GoToAsync("..");
            }, "Збереження...");
        }
    }
}