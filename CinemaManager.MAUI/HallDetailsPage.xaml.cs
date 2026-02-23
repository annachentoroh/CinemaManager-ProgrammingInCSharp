using CinemaManager.DB;
using CinemaManager.UI;

namespace CinemaManager.MAUI;

[QueryProperty(nameof(Hall), "SelectedHall")]
public partial class HallDetailsPage : ContentPage
{
    private readonly ICinemaRepo _storageService;
    private CinemaHallUI _hall;

    public CinemaHallUI Hall
    {
        get => _hall;
        set
        {
            _hall = value;
            LoadHallData();
        }
    }

    public HallDetailsPage(ICinemaRepo storageService)
    {
        InitializeComponent();
        _storageService = storageService;
    }

    private void LoadHallData()
    {
        if (_hall == null) return;

        HallNameLabel.Text = _hall.Name;
        HallInfoLabel.Text = $"Тип: {_hall.HallType} | Місць: {_hall.SeatsCount}";
        TotalDurationLabel.Text = $"Загальний час фільмів у залі: {_hall.TotalDurationMinutes} хв";

        // Завантаження сеансів через сервіс
        var sessionEntities = _storageService.GetSessionsByHallId(_hall.Id);
        _hall.Sessions = sessionEntities.Select(s => new MovieSessionUI(s)).ToList();

        SessionsCollection.ItemsSource = _hall.Sessions;
    }

    private async void OnSessionSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is MovieSessionUI selectedSession)
        {
            var parameters = new Dictionary<string, object> { { "SelectedSession", selectedSession } };
            await Shell.Current.GoToAsync(nameof(SessionDetailsPage), parameters); // Перехід на деталі сеансу
            SessionsCollection.SelectedItem = null;
        }
    }
}