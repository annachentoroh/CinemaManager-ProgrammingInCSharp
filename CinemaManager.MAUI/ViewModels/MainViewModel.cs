using CinemaManager.Services;
using CinemaManager.Services.DTO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CinemaManager.MAUI.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private readonly ICinemaService _cinemaService;

        public ObservableCollection<CinemaHallListDTO> Halls { get; set; }

        private CinemaHallListDTO _selectedHall;
        public CinemaHallListDTO SelectedHall
        {
            get => _selectedHall;
            set { _selectedHall = value; OnPropertyChanged(); }
        }

        public ICommand HallSelectedCommand { get; }

        public MainViewModel(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
            Halls = new ObservableCollection<CinemaHallListDTO>(_cinemaService.GetAllHalls());
            HallSelectedCommand = new Command(LoadHall);
        }

        private void LoadHall()
        {
            if (SelectedHall == null) return;

            // Надійний спосіб №1: Передача через словник
            var parameters = new Dictionary<string, object>
            {
                { "HallId", SelectedHall.Id }
            };

            Shell.Current.GoToAsync($"{nameof(HallDetailsPage)}", parameters);

            SelectedHall = null;
        }
    }
}