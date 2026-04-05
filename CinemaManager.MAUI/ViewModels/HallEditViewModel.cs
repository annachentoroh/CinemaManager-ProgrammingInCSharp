using CinemaManager.Models.Enums;
using CinemaManager.Services;
using CinemaManager.Services.DTO;
using System.Windows.Input;

namespace CinemaManager.MAUI.ViewModels
{
    [QueryProperty(nameof(HallId), "hallId")]
    public class HallEditViewModel : BaseViewModel
    {
        private readonly ICinemaService _service;

        private string? _hallId;
        public string? HallId
        {
            get => _hallId;
            set { _hallId = value; OnPropertyChanged(); }
        }

        public bool IsEditMode => !string.IsNullOrEmpty(HallId);
        public string PageTitle => IsEditMode ? "Редагувати зал" : "Новий зал";

        // Форма
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _totalSeats = 50;
        public int TotalSeats
        {
            get => _totalSeats;
            set => SetProperty(ref _totalSeats, value);
        }

        private string _selectedHallType = "TwoD";
        public string SelectedHallType
        {
            get => _selectedHallType;
            set => SetProperty(ref _selectedHallType, value);
        }

        public List<string> HallTypes { get; } = Enum.GetNames<HallType>().ToList();

        public ICommand LoadCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public HallEditViewModel(ICinemaService service)
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
                var details = await _service.GetHallDetailsAsync(Guid.Parse(HallId!));
                if (details == null) return;
                Name = details.Name;
                TotalSeats = details.TotalSeats;
                SelectedHallType = details.HallType;
            });
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Помилка", "Введіть назву залу", "OK");
                return;
            }

            await ExecuteBusyAsync(async () =>
            {
                if (IsEditMode)
                {
                    await _service.UpdateHallAsync(new CinemaHallUpdateDTO
                    {
                        Id = Guid.Parse(HallId!),
                        Name = Name,
                        TotalSeats = TotalSeats,
                        HallType = SelectedHallType
                    });
                }
                else
                {
                    await _service.CreateHallAsync(new CinemaHallCreateDTO
                    {
                        Name = Name,
                        TotalSeats = TotalSeats,
                        HallType = SelectedHallType
                    });
                }
                await Shell.Current.GoToAsync("..");
            }, "Збереження...");
        }
    }
}