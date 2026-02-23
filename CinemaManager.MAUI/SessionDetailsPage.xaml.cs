using CinemaManager.UI;

namespace CinemaManager.MAUI;

[QueryProperty(nameof(Session), "SelectedSession")]
public partial class SessionDetailsPage : ContentPage
{
    private MovieSessionUI _session;

    public MovieSessionUI Session
    {
        get => _session;
        set
        {
            _session = value;
            UpdateUI();
        }
    }

    public SessionDetailsPage()
    {
        InitializeComponent();
    }

    private void UpdateUI()
    {
        if (_session == null) return;

        MovieTitleLabel.Text = _session.MovieTitle;
        YearLabel.Text = $"Рік випуску: {(_session as dynamic)._entity.ReleaseYear}";
        GenreLabel.Text = $"Жанр: {_session.Genre}";
        DurationLabel.Text = $"Тривалість: {_session.DurationMinutes} хвилин";
        TimeLabel.Text = $"Час показу: {_session.StartTime:HH:mm} - {_session.EndTime:HH:mm}";
    }
}