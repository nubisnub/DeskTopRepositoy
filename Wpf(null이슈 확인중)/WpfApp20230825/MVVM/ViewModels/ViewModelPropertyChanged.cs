using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp20230825.ViewModels
{
    public class ViewModelPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
