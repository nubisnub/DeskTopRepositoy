using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfPredictScore.ViewModels
{
    public class ViewModelINotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChange([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}