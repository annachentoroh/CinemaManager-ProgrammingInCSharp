using CinemaManager.DB;

namespace CinemaManager.MAUI;

public partial class App : Application
{
    private readonly DatabaseInitializer _initializer;

    public App(DatabaseInitializer initializer)
    {
        InitializeComponent();
        _initializer = initializer;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Task.Run(async () => await _initializer.InitializeAsync()).Wait();
        return new Window(new AppShell());
    }
}