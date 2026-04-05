using CinemaManager.Services;
using CinemaManager.Services.DTO;
using System.Windows.Input;

namespace CinemaManager.MAUI.ViewModels
{
    [QueryProperty(nameof(SessionId), "sessionId")]
    public class SessionDetailsViewModel : BaseViewModel
    {
        private readonly ICinemaService _service;

        private string? _sessionId;
        public string? SessionId
        {
            get => _sessionId;
            set { _sessionId = value; OnPropertyChanged(); }
        }

        private MovieSessionDetailsDTO? _sessionDetails;
        public MovieSessionDetailsDTO? SessionDetails
        {
            get => _sessionDetails;
            set => SetProperty(ref _sessionDetails, value);
        }

        public ICommand LoadCommand { get; }
        public ICommand EditCommand { get; }

        public SessionDetailsViewModel(ICinemaService service)
        {
            _service = service;
            LoadCommand = new Command(async () => await LoadAsync());
            EditCommand = new Command(async () => await EditAsync());
        }

        public async Task LoadAsync()
        {
            if (string.IsNullOrEmpty(SessionId)) return;
            await ExecuteBusyAsync(async () =>
            {
                SessionDetails = await _service.GetSessionDetailsAsync(Guid.Parse(SessionId));
            });
        }

        private async Task EditAsync()
        {
            if (SessionDetails == null) return;
            await Shell.Current.GoToAsync(
                $"{nameof(SessionEditPage)}?sessionId={SessionDetails.Id}&hallId={SessionDetails.CinemaHallId}");
        }
    }
}