using CinemaManager.DB;
using CinemaManager.UI;

namespace CinemaManager.MAUI;

public partial class MainPage : ContentPage
{
    private readonly ICinemaRepo _storageService;

    public MainPage(ICinemaRepo storageService)
    {
        InitializeComponent();
        _storageService = storageService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var hallEntities = _storageService.GetAllHalls();
        var uiHalls = hallEntities.Select(h => new CinemaHallUI(h)).ToList();
        HallsCollection.ItemsSource = uiHalls;
    }

    private async void OnHallSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is CinemaHallUI selectedHall)
        {
            var parameters = new Dictionary<string, object> { { "SelectedHall", selectedHall } };
            await Shell.Current.GoToAsync(nameof(HallDetailsPage), parameters); // Перехід на деталі залу
            HallsCollection.SelectedItem = null; // Скидання виділення
        }
    }
}