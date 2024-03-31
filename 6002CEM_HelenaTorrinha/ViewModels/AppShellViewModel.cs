using System.Windows.Input;
using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class AppShellViewModel : BaseViewModel
{
    private readonly IAppState _state;
    public ICommand LogOutCommand { get; set; }
    public ICommand MyRecipesCommand { get; set; }
    public ICommand AccountCommand { get; set; }

    public AppShellViewModel(ViewModelContext context, IAppState state) : base(context)
    {
        _state = state;
        LogOutCommand = new Command(ExecuteLogout);
        MyRecipesCommand = new Command(NavigateToMyRecipes);
        AccountCommand = new Command(NavigateToUserPage);
    }

    private void ExecuteLogout()
    {
        _state.CurrentUser = null;
        Models.User.Clear();
        Shell.Current.GoToAsync("//login");
    }

    private async void NavigateToMyRecipes()
    {
        await Shell.Current.GoToAsync("/MyRecipesPage");
    }

    private async void NavigateToUserPage()
    {
        await Shell.Current.GoToAsync("/UserPage");
    }
}