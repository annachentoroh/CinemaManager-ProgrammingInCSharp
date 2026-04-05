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

        // Шлях до SQLite БД
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "cinema.db");

        // Реєстрація DB шару
        builder.Services.AddSingleton(new DbContextFactory(dbPath));
        builder.Services.AddSingleton<DatabaseInitializer>();
        builder.Services.AddSingleton<ICinemaRepo, CinemaRepo>();

        // Реєстрація сервісів
        builder.Services.AddSingleton<ICinemaService, CinemaService>();

        // ViewModels
        builder.Services.AddTransient<ViewModels.MainViewModel>();
        builder.Services.AddTransient<ViewModels.HallDetailsViewModel>();
        builder.Services.AddTransient<ViewModels.SessionDetailsViewModel>();
        builder.Services.AddTransient<ViewModels.HallEditViewModel>();
        builder.Services.AddTransient<ViewModels.SessionEditViewModel>();

        // Pages
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<HallDetailsPage>();
        builder.Services.AddTransient<SessionDetailsPage>();
        builder.Services.AddTransient<HallEditPage>();
        builder.Services.AddTransient<SessionEditPage>();

        return builder.Build();
    }
}