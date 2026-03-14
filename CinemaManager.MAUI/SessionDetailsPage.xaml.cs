using CinemaManager.MAUI.ViewModels;

namespace CinemaManager.MAUI;

public partial class SessionDetailsPage : ContentPage
{
    public SessionDetailsPage(SessionDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}