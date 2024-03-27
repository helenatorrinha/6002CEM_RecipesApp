
using System.Windows.Input;
using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Views;
using _6002CEM_HelenaTorrinha.Models;
using Newtonsoft.Json;
using Supabase;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class AddRecipePageViewModel : BaseViewModel
{
    private readonly Client _supabaseClient;
    private readonly IAppState _appState;

    public ICommand AddRecipeCommand { get; set; }
    public ICommand NavigateToRegisterPageCommand { get; set; }

    public string Name
    {
        get => Get<string>();
        set
        {
            Set(value);
            (AddRecipeCommand as Command).ChangeCanExecute();
        }
    }
    public int Preptime
    {
        get => Get<int>();
        set
        {
            Set(value);
            (AddRecipeCommand as Command).ChangeCanExecute();
        }
    }
    public int Cooktime
    {
        get => Get<int>();
        set
        {
            Set(value);
            (AddRecipeCommand as Command).ChangeCanExecute();
        }
    }
    public int Servings
    {
        get => Get<int>();
        set
        {
            Set(value);
            (AddRecipeCommand as Command).ChangeCanExecute();
        }
    }
    public string Ingredients
    {
        get => Get<string>();
        set
        {
            Set(value);
            (AddRecipeCommand as Command).ChangeCanExecute();
        }
    }
    public string Instructions
    {
        get => Get<string>();
        set
        {
            Set(value);
            (AddRecipeCommand as Command).ChangeCanExecute();
        }
    }

    public AddRecipePageViewModel(ViewModelContext context, IAppState appState) : base(context)
    {
        _appState = appState; // Assuming you want to store this for future use
        _supabaseClient = new Client(SupabaseDetails.Url, SupabaseDetails.SupabaseKey);
        AddRecipeCommand = new Command(execute: async () => await AddRecipe(),
                                       canExecute: () => !string.IsNullOrEmpty(Name) && int.IsPositive(Preptime) &&
                                                         int.IsPositive(Cooktime) && int.IsPositive(Servings) &&
                                                         !string.IsNullOrEmpty(Ingredients) && !string.IsNullOrEmpty(Instructions));
    }


    public override void OnAppearing()
    {
        base.OnAppearing();
        Name = string.Empty;
        Preptime = 0;
        Cooktime = 0;
        Servings = 0;
        Ingredients = string.Empty;
        Instructions = string.Empty;
    }

    private async Task AddRecipe()
    {
        
        //Check if the user filled all the fields
        if (string.IsNullOrEmpty(Name) || int.IsNegative(Preptime) || int.IsNegative(Cooktime) ||
            int.IsNegative(Servings) || string.IsNullOrEmpty(Ingredients) || string.IsNullOrEmpty(Instructions))
        {
            await Shell.Current.DisplayAlert("Error", "Make sure you fill all fields.", "OK");
            return;
        }
        

        try
        {
            //The recipe we want to insert in 'recipes' table
            var recipe = new RecipeDatabase
            {
                userID = Models.User.Instance.UserID,
                name = Name,
                prepTime = Preptime,
                cookTime = Cooktime,
                servings = Servings,
                ingredients = Ingredients,
                instructions = Instructions
            };
            try
            {
                //Insert the user in the 'users' table
                var databaseResponse = await _supabaseClient.From<RecipeDatabase>().Insert(recipe);
                if (databaseResponse == null)
                {
                    await Shell.Current.DisplayAlert("Error", "Recipe not created", "OK");
                }

                // Redirect to login page if successful
                await Shell.Current.DisplayAlert("Success", "Recipe added sucessfully", "OK");
                await Shell.Current.Navigation.PopAsync();
                await Shell.Current.GoToAsync("//RecipesPage");
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
                await Shell.Current.DisplayAlert("Add recipe failed", errorMessage, "OK");
            }
            else
            {
                // Fallback
                await Shell.Current.DisplayAlert("Add recipe failed", "An error occurred", "OK");
            }
        }

    }

    private async Task NavigateToRegisterPage()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}
