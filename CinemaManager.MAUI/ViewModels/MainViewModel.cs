using CinemaManager.Services;
using CinemaManager.Services.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaManager.MAUI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ICinemaService _service;

        // Повний список (для фільтрації)
        private List<CinemaHallListDTO> _allHalls = new();

        private ObservableCollection<CinemaHallListDTO> _halls = new();
        public ObservableCollection<CinemaHallListDTO> Halls
        {
            get => _halls;
            set => SetProperty(ref _halls, value);
        }

        private CinemaHallListDTO? _selectedHall;
        public CinemaHallListDTO? SelectedHall
        {
            get => _selectedHall;
            set => SetProperty(ref _selectedHall, value);
        }

        // Пошук
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set { SetProperty(ref _searchText, value); ApplyFilter(); }
        }

        // Сортування
        private string _sortOption = "Назва ↑";
        public string SortOption
        {
            get => _sortOption;
            set { SetProperty(ref _sortOption, value); ApplyFilter(); }
        }

        public List<string> SortOptions { get; } = new()
        {
            "Назва ↑", "Назва ↓", "Місць ↑", "Місць ↓"
        };

        // Команди
        public ICommand LoadCommand { get; }
        public ICommand HallSelectedCommand { get; }
        public ICommand AddHallCommand { get; }
        public ICommand DeleteHallCommand { get; }
        public ICommand EditHallCommand { get; }

        public MainViewModel(ICinemaService service)
        {
            _service = service;
            LoadCommand = new Command(async () => await LoadAsync());
            HallSelectedCommand = new Command(async () => await NavigateToHallAsync());
            AddHallCommand = new Command(async () => await AddHallAsync());
            DeleteHallCommand = new Command<CinemaHallListDTO>(async (h) => await DeleteHallAsync(h));
            EditHallCommand = new Command<CinemaHallListDTO>(async (h) => await EditHallAsync(h));
        }

        public async Task LoadAsync()
        {
            await ExecuteBusyAsync(async () =>
            {
                var halls = await _service.GetAllHallsAsync();
                _allHalls = halls.ToList();
                ApplyFilter();
            });
        }

        private void ApplyFilter()
        {
            var filtered = _allHalls
                .Where(h => string.IsNullOrWhiteSpace(SearchText) ||
                            h.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            filtered = SortOption switch
            {
                "Назва ↑" => filtered.OrderBy(h => h.Name).ToList(),
                "Назва ↓" => filtered.OrderByDescending(h => h.Name).ToList(),
                "Місць ↑" => filtered.OrderBy(h => h.TotalSeats).ToList(),
                "Місць ↓" => filtered.OrderByDescending(h => h.TotalSeats).ToList(),
                _ => filtered
            };

            Halls = new ObservableCollection<CinemaHallListDTO>(filtered);
        }

        private async Task NavigateToHallAsync()
        {
            if (SelectedHall == null) return;
            var id = SelectedHall.Id;
            SelectedHall = null; // скидаємо виділення
            await Shell.Current.GoToAsync($"{nameof(HallDetailsPage)}?hallId={id}");
        }

        private async Task AddHallAsync()
        {
            await Shell.Current.GoToAsync(nameof(HallEditPage));
        }

        private async Task DeleteHallAsync(CinemaHallListDTO hall)
        {
            bool confirm = await Shell.Current.DisplayAlert(
                "Видалення", $"Видалити зал '{hall.Name}'? Всі сеанси також будуть видалені.", "Так", "Ні");
            if (!confirm) return;

            await ExecuteBusyAsync(async () =>
            {
                await _service.DeleteHallAsync(hall.Id);
                await LoadAsync();
            }, "Видалення...");
        }

        private async Task EditHallAsync(CinemaHallListDTO hall)
        {
            await Shell.Current.GoToAsync($"{nameof(HallEditPage)}?hallId={hall.Id}");
        }
    }
}