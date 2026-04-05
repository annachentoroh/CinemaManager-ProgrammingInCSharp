using CinemaManager.MAUI.ViewModels;

namespace CinemaManager.MAUI;

public partial class HallEditPage : ContentPage
{
    private readonly HallEditViewModel _viewModel;

    public HallEditPage(HallEditViewModel viewModel)
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