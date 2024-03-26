using System.Windows.Input;
using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Models;
using Newtonsoft.Json;
using Supabase;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class RegisterPageViewModel : BaseViewModel
{
    private readonly Client _supabaseClient;
    public ICommand RegisterCommand { get; set; }

    public string Username
    {
        get => Get<string>();
        set
        {
            Set(value);
            (RegisterCommand as Command).ChangeCanExecute();
        }
    }

    public string firstName
    {
        get => Get<string>();
        set
        {
            Set(value);
            (RegisterCommand as Command).ChangeCanExecute();
        }
    }

    public string lastName
    {
        get => Get<string>();
        set
        {
            Set(value);
            (RegisterCommand as Command).ChangeCanExecute();
        }
    }

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

    public string ConfirmPassword
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

        RegisterCommand = new Command(execute: async () => await Register(),
                               canExecute: () => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(firstName) &&
                               !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(Email)
                               && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword));
    }

    private async Task Register()
    {
        //Check if the user filled all the fields
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) ||
            string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
        {
            await Shell.Current.DisplayAlert("Error", "Make sure you fill all fields.", "OK");
            return;
        }

        // Check if Password and ConfirmPassword are the same
        if (Password != ConfirmPassword)
        {
            await Shell.Current.DisplayAlert("Registration Error", "Passwords do not match.", "OK");
            return; // Exit the method early if the passwords don't match
        }

        try
        {
            // Register the user in Supabase
            var authenticationResponse = await _supabaseClient.Auth.SignUp(Email, Password);

            //The user we want to insert in 'users' table
            var user = new UserDatabase
            {
                username = Username,
                firstName = firstName,
                lastName = lastName,
                email = Email
            };

            try
            {
                //Insert the user in the 'users' table
                var databaseResponse = await _supabaseClient.From<UserDatabase>().Insert(user);
                if (databaseResponse == null)
                {
                    await Shell.Current.DisplayAlert("Error", "User not created", "OK");
                }

                // Redirect to login page if successful
                await Shell.Current.DisplayAlert("Sign up successful", "You can log in now", "OK");
                await Shell.Current.Navigation.PopAsync();
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "OK");
            }
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
