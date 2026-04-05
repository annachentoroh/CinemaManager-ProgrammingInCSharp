using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CinemaManager.MAUI.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private string _busyMessage = "Завантаження...";

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsNotBusy)); }
        }

        public bool IsNotBusy => !_isBusy;

        public string BusyMessage
        {
            get => _busyMessage;
            set { _busyMessage = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(name);
        }

        // Захищене виконання async з IsBusy
        protected async Task ExecuteBusyAsync(Func<Task> action, string message = "Завантаження...")
        {
            if (IsBusy) return;
            try
            {
                BusyMessage = message;
                IsBusy = true;
                await action();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}