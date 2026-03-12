using CinemaManager.Services;
using CinemaManager.UI;

namespace CinemaManager.MAUI;

public partial class MainPage : ContentPage
{
    // Тепер використовуємо сервіс замість репозиторію
    private readonly ICinemaService _cinemaService;

    // Впроваджуємо сервіс через конструктор
    public MainPage(ICinemaService cinemaService)
    {
        InitializeComponent();
        _cinemaService = cinemaService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Отримуємо дані через сервіс (DTO)
        var hallDtos = _cinemaService.GetAllHalls();

        var uiHalls = hallDtos.Select(dto => {
            var details = _cinemaService.GetHallDetails(dto.Id);
            return new CinemaHallUI(new Models.Entities.CinemaHall(
                details.Id, details.Name, details.TotalSeats, Models.Enums.HallType.TwoD)
            );
        }).ToList();

        HallsCollection.ItemsSource = uiHalls;
    }

    private async void OnHallSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is CinemaHallUI selectedHall)
        {
            var parameters = new Dictionary<string, object> { { "SelectedHall", selectedHall } };
            await Shell.Current.GoToAsync(nameof(HallDetailsPage), parameters);
            HallsCollection.SelectedItem = null;
        }
    }
}