namespace CinemaManager.MAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Реєструємо маршрути для навігації
        Routing.RegisterRoute(nameof(HallDetailsPage), typeof(HallDetailsPage));
        Routing.RegisterRoute(nameof(SessionDetailsPage), typeof(SessionDetailsPage));
    }
}