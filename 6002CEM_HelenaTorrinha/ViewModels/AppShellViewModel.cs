using System.Windows.Input;
using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class AppShellViewModel : BaseViewModel
{
    private readonly IAppState _state;
    public ICommand LogOutCommand { get; set; }

    public AppShellViewModel(ViewModelContext context, IAppState state) : base(context)
    {
        _state = state;
        LogOutCommand = new Command(ExecuteLogout);
    }

    private void ExecuteLogout()
    {
        _state.CurrentUser = null;
        Models.User.Clear();
        Shell.Current.GoToAsync("//login");
    }
}