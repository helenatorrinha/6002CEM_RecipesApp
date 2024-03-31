using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using Supabase;
using Supabase.Gotrue;
using _6002CEM_HelenaTorrinha.Models;
using System.Windows.Input;
using Newtonsoft.Json;

namespace _6002CEM_HelenaTorrinha.ViewModels
{
    public class UserPageViewModel : BaseViewModel
    {
        private readonly Supabase.Client _supabaseClient;
        public ICommand UserCommand { get; set; }
        private readonly IAppState _appState;

        public string username
        {
            get => Get<string>();
            set => Set(value);
        }

        public string firstName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string lastName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string email
        {
            get => Get<string>();
            set => Set(value);
        }

        public string CurrentPassword
        {
            get => Get<string>();
            set
            {
                Set(value);
                (UserCommand as Command).ChangeCanExecute();
            }
        }

        public string NewPassword
        {
            get => Get<string>();
            set
            {
                Set(value);
                (UserCommand as Command).ChangeCanExecute();
            }
        }


        public UserPageViewModel(ViewModelContext context, IAppState appState) : base(context)
        {
            _supabaseClient = new Supabase.Client(SupabaseDetails.Url, SupabaseDetails.SupabaseKey);
            Task.Run(() => GetPage());
            UserCommand = new Command(execute: async () => await UpdatePassword(),
            () => !string.IsNullOrEmpty(CurrentPassword) && !string.IsNullOrEmpty(NewPassword));
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            CurrentPassword = string.Empty;
            NewPassword = string.Empty;
        }

        private async Task GetPage()
        {
            // Fetching recipes from the 'recipes' table
            var databaseResponse = await _supabaseClient.From<UserDatabase>().Where(u => u.id == Models.User.Instance.UserID).Get();
            if (databaseResponse.Models != null && databaseResponse.Models.Count > 0)
            {
                username = databaseResponse.Models[0].username;
                firstName = databaseResponse.Models[0].firstName;
                lastName = databaseResponse.Models[0].lastName;
                email = databaseResponse.Models[0].email;
            }
        }

        private async Task UpdatePassword()
        {
            try
            {
                //Try to login the user with the currentPassword to see if that password is correct
                var signInResponse = await _supabaseClient.Auth.SignIn(Models.User.Instance.Email, CurrentPassword);

                if (signInResponse != null)
                {
                    var updatedPassword = new UserAttributes { Password = NewPassword };
                    var updateResponse = await _supabaseClient.Auth.Update(updatedPassword);

                    if (updateResponse != null)
                    {
                        await Shell.Current.DisplayAlert("Password updated", "Your password has been sucessfully updated.", "OK");
                        await Shell.Current.GoToAsync("//login");
                    }
                    
                }
            }

            catch (Supabase.Gotrue.Exceptions.GotrueException e)
            {
                var errorData = JsonConvert.DeserializeObject<Dictionary<string, string>>(e.Message);

                if (errorData.ContainsKey("error") && errorData["error"] == "invalid_grant")
                {
                    await Shell.Current.DisplayAlert("Error", "Invalid current password.", "OK");
                }
            }
        }
    }
}

