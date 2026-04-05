using CinemaManager.MAUI.ViewModels;

namespace CinemaManager.MAUI;

public partial class SessionEditPage : ContentPage
{
    private readonly SessionEditViewModel _viewModel;

    public SessionEditPage(SessionEditViewModel viewModel)
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