
using System.Windows.Input;
using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Views;
using Newtonsoft.Json;
using Supabase;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class LoginPageViewModel : BaseViewModel
{
    private readonly Client _supabaseClient;
    private readonly IAppState _appState;

    public ICommand LoginCommand { get; set; }
    public ICommand NavigateToRegisterPageCommand { get; set; }

    public string Email
    {
        get => Get<string>();
        set
        {
            Set(value);
            (LoginCommand as Command).ChangeCanExecute();
        }
    }
    public string Password
    {
        get => Get<string>();
        set
        {
            Set(value);
            (LoginCommand as Command).ChangeCanExecute();
        }
    }

    public LoginPageViewModel(ViewModelContext context, IAppState appState): base(context)
    {
        _supabaseClient = new Client(SupabaseDetails.Url, SupabaseDetails.SupabaseKey);
        _appState = appState;
        LoginCommand = new Command(execute: async () => await Login(), 
            () => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password));
        NavigateToRegisterPageCommand = new Command(async () => await NavigateToRegisterPage());
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        Email = string.Empty;
        Password = string.Empty;
    }

    private async Task Login()
    {
        try
        {
            // Set the current user in the app state
            _appState.CurrentUser = new Models.User()
            {
                Email = Email,
                Password = Password
            };
            
            // Sign in with email and password to Supabase
            var response = await _supabaseClient.Auth.SignIn(Email, Password);
            
            // go to articles page if login is successful
            await Shell.Current.GoToAsync("//AddRecipePage");
        }
        // Handle invalid email or password error from Supabase
        catch (Supabase.Gotrue.Exceptions.GotrueException e)
        {
            await Shell.Current.DisplayAlert("Error", "Invalid email or password.", "OK");
        } 
        // Handle other exceptions
        catch (Exception e)
        {
            Console.WriteLine($"Error Message: {e.Message}");
            await Shell.Current.DisplayAlert("Sign in failed", "Error occured. Please try again or register for an account.", "OK");
        }
    }
    
    private async Task NavigateToRegisterPage()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}


