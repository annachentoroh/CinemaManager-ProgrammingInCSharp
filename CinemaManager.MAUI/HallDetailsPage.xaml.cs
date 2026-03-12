using CinemaManager.Services;
using CinemaManager.UI;
using CinemaManager.Models.Entities;

namespace CinemaManager.MAUI;

[QueryProperty(nameof(Hall), "SelectedHall")]
public partial class HallDetailsPage : ContentPage
{
    // 1. Змінюємо репозиторій на сервіс
    private readonly ICinemaService _cinemaService;
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

    // 2. Впроваджуємо сервіс через конструктор
    public HallDetailsPage(ICinemaService cinemaService)
    {
        InitializeComponent();
        _cinemaService = cinemaService;
    }

    private void LoadHallData()
    {
        if (_hall == null) return;
        HallNameLabel.Text = _hall.Name;
        HallInfoLabel.Text = $"Тип: {_hall.HallType} | Місць: {_hall.SeatsCount}";

        // Завантаження деталей залу та сеансів через сервіс (використовуємо DTO)
        var hallDetails = _cinemaService.GetHallDetails(_hall.Id);

        if (hallDetails != null)
        {
            // Конвертуємо MovieSessionListDTO з сервісу назад у MovieSessionUI для відображення
            _hall.Sessions = hallDetails.Sessions.Select(s => new MovieSessionUI(new MovieSession(
                s.Id,
                _hall.Id,
                s.MovieTitle,
                Models.Enums.Genre.Drama,
                2024,
                s.StartTime,
                120))).ToList();

            TotalDurationLabel.Text = $"Загальний час фільмів у залі: {_hall.TotalDurationMinutes} хв";
            SessionsCollection.ItemsSource = _hall.Sessions;
        }
    }

    private async void OnSessionSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is MovieSessionUI selectedSession)
        {
            var parameters = new Dictionary<string, object> { { "SelectedSession", selectedSession } };
            await Shell.Current.GoToAsync(nameof(SessionDetailsPage), parameters);
            SessionsCollection.SelectedItem = null;
        }
    }
}