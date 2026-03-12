using CinemaManager.DB;
using CinemaManager.Services;


namespace CinemaManager.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Реєстрація сервісів (Dependency Injection)
        builder.Services.AddSingleton<ICinemaRepo, CinemaRepo>();
        builder.Services.AddSingleton<ICinemaService, CinemaService>();

        // Реєстрація сторінок
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<HallDetailsPage>();
        builder.Services.AddTransient<SessionDetailsPage>();

        return builder.Build();
    }
}