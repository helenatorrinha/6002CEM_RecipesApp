using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using Supabase;
using _6002CEM_HelenaTorrinha.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using _6002CEM_HelenaTorrinha.Views;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class RecipesPageViewModel : BaseViewModel
{
    private readonly Client _supabaseClient;
    public ObservableCollection<RecipeDatabase> Recipes { get; set; } = new ObservableCollection<RecipeDatabase>();

    public RecipeDatabase recipe { get; set; }
    public string name { get; set; }
    public int prepTime { get; set; }
    public int cookTime { get; set; }
    public int servings { get; set; }
    public string ingredients { get; set; }
    public string instructions { get; set; }

    public ICommand NavigateToMyRecipesPageCommand { get; set; }

    public RecipesPageViewModel(ViewModelContext context, IAppState appState) : base(context)
    {
        _supabaseClient = new Client(SupabaseDetails.Url, SupabaseDetails.SupabaseKey);
        Task.Run(() => GetPage());
        NavigateToMyRecipesPageCommand = new Command(async () => await NavigateToMyRecipesPage());
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(() => GetPage());
    }

    private async Task GetPage()
    {
        // Fetching recipes from the 'recipes' table
        var databaseResponse = await _supabaseClient.From<RecipeDatabase>().Get();
        if (databaseResponse.Models != null && databaseResponse.Models.Count > 0)
        {
            Recipes.Clear();
            foreach (var recipe in databaseResponse.Models)
            {
                Recipes.Add(recipe);
            }
        }
    }

    private async Task NavigateToMyRecipesPage()
    {
        await Shell.Current.GoToAsync("/MyRecipesPage");
    }
}
