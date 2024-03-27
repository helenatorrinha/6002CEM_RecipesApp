using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using Supabase;
using _6002CEM_HelenaTorrinha.Models;
using System.Collections.ObjectModel;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class MyRecipesPageViewModel : BaseViewModel
{
    private readonly Client _supabaseClient;
    public ObservableCollection<RecipeDatabase> MyRecipes { get; set; } = new ObservableCollection<RecipeDatabase>();

    public RecipeDatabase recipe { get; set; }
    public string name { get; set; }
    public int prepTime { get; set; }
    public int cookTime { get; set; }
    public int servings { get; set; }
    public string ingredients { get; set; }
    public string instructions { get; set; }

    public MyRecipesPageViewModel(ViewModelContext context, IAppState appState) : base(context)
    {
        _supabaseClient = new Client(SupabaseDetails.Url, SupabaseDetails.SupabaseKey);
        Task.Run(() => GetPage());
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async Task GetPage()
    {

        // Fetching recipes from the 'recipes' table
        var databaseResponse = await _supabaseClient.From<RecipeDatabase>().Where(u => u.userID == Models.User.Instance.UserID).Get();
       
        if (databaseResponse.Models != null && databaseResponse.Models.Count > 0)
        {
            MyRecipes.Clear();
            foreach (var recipe in databaseResponse.Models)
            {
                MyRecipes.Add(recipe);
            }
        }
    }
}
