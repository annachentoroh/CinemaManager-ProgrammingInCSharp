using CinemaManager.MAUI.ViewModels;

namespace CinemaManager.MAUI;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; // Підключаємо ViewModel до сторінки
    }
}