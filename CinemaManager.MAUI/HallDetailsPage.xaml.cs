using CinemaManager.MAUI.ViewModels;

namespace CinemaManager.MAUI;

public partial class HallDetailsPage : ContentPage
{
    private readonly HallDetailsViewModel _viewModel;

    public HallDetailsPage(HallDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}