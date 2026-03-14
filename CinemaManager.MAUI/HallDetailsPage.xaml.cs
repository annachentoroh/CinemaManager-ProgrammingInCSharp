using CinemaManager.MAUI.ViewModels;

namespace CinemaManager.MAUI;

public partial class HallDetailsPage : ContentPage
{
    public HallDetailsPage(HallDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}