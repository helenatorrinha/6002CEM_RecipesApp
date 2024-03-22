using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class BaseViewModelMoreSimple : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public BaseViewModelMoreSimple()
    {
    }
}