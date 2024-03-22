using System.Windows.Input;
using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using Newtonsoft.Json;
using Supabase;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class RegisterPageViewModel : BaseViewModel
{
    private readonly Client _supabaseClient;
    public ICommand RegisterCommand { get; set; }

    public string Email
    {
        get => Get<string>();
        set
        {
            Set(value);
            (RegisterCommand as Command).ChangeCanExecute();
        }
    }
    public string Password
    {
        get => Get<string>();
        set
        {
            Set(value);
            (RegisterCommand as Command).ChangeCanExecute();
        }
    }

    public RegisterPageViewModel(ViewModelContext context, IAppState appState) : base(context)
    {
        _supabaseClient = new Client(SupabaseDetails.Url, SupabaseDetails.SupabaseKey);
        RegisterCommand = new Command(async () => await Register(),
            () => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password));
    }

    private async Task Register()
    {
        try
        {
            // Register the user
            var response = await _supabaseClient.Auth.SignUp(Email, Password);
            // go back to login page if successful
            await Shell.Current.DisplayAlert("Sign up successful", "You can now log in", "OK");
            await Shell.Current.Navigation.PopAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error Message: {e.Message}");

            // Parse JSON error to get the error message
            var errorData = JsonConvert.DeserializeObject<Dictionary<string, object>>(e.Message);

            if (errorData.ContainsKey("msg"))
            {
                var errorMessage = errorData["msg"].ToString();
                await Shell.Current.DisplayAlert("Registration failed", errorMessage, "OK");
            }
            else
            {
                // Fallback
                await Shell.Current.DisplayAlert("Registration failed", "An error occurred", "OK");
            }
        }
    }
}