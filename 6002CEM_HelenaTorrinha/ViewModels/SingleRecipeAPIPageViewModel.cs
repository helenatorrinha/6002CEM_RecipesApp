using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using Supabase;
using _6002CEM_HelenaTorrinha.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Newtonsoft.Json;


namespace _6002CEM_HelenaTorrinha.ViewModels;

[QueryProperty(nameof(id), "id")]

public class SingleRecipeAPIPageViewModel : BaseViewModel
{
    private readonly Client _supabaseClient;
    public ObservableCollection<RecipeAPI> Recipes { get; set; } = new ObservableCollection<RecipeAPI>();
    private readonly HttpClient _httpClient = new HttpClient();

    public int id
    {
        get => Get<int>();
        set
        {
            Set(value);
        }
    }

    public string title
    {
        get => Get<string>();
        set
        {
            Set(value);
        }
    }

    public int readyInMinutes
    {
        get => Get<int>();
        set
        {
            Set(value);
        }
    }

    public int servings
    {
        get => Get<int>();
        set
        {
            Set(value);
        }
    }

    public string ingredients
    {
        get => Get<string>();
        set
        {
            Set(value);
        }
    }

    public string sourceUrl
    {
        get => Get<string>();
        set
        {
            Set(value);
        }
    }

    public string image
    {
        get => Get<string>();
        set
        {
            Set(value);
        }
    }

    public ICommand NavigateToMyRecipesPageCommand { get; set; }

    public ICommand ItemTappedCommand { get; private set; }

    public SingleRecipeAPIPageViewModel(ViewModelContext context, IAppState appState) : base(context)
    {
        _supabaseClient = new Client(SupabaseDetails.Url, SupabaseDetails.SupabaseKey);
        ItemTappedCommand = new Command(OnItemTapped);
    }

    public override async void OnAppearing()
    {
        base.OnAppearing();
        await GetPage();
    }

    private async Task GetPage()
    {
        try
        {
            Console.WriteLine(id);
            var apiKey = SpoonacularDetails.SpoonacularKey; //The api key

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spoonacular.com/recipes/{id}/information?apiKey={apiKey}"); 

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response
            var recipesResponse = JsonConvert.DeserializeObject<Recipe>(jsonResponse);

            // Update the ObservableCollection with the new data
            Recipes.Clear();
            if (recipesResponse != null)
            {
                // Map the properties from the API response to the RecipeDatabase model
                title = recipesResponse.Title;
                readyInMinutes = recipesResponse.ReadyInMinutes;
                servings = recipesResponse.Servings;
                ingredients = string.Join(", ", recipesResponse.extendedIngredients.Select(ing => ing.original)); 
                sourceUrl = recipesResponse.sourceUrl;
                image = recipesResponse.image;
            
            }

            Recipes = new ObservableCollection<RecipeAPI>(Recipes);
            Console.WriteLine(jsonResponse); // For debugging, consider removing or replacing with appropriate logging
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching recipes: {ex.Message}");
            // Handle exceptions or errors as appropriate
        }
    }

    public class Recipe
    {
        // Define properties according to Spoonacular's JSON response
        public string Title { get; set; }
        public int ReadyInMinutes { get; set; }
        public int Servings { get; set; }
        public string sourceUrl { get; set; }
        public List<Ingredient> extendedIngredients { get; set; }
        public string image { get; set; }
    }

    public class Ingredient
    {
        public string original { get; set; }
    }

    private async void OnItemTapped()
    {
        try
        {
            await Launcher.OpenAsync(new Uri(sourceUrl));
        }
        catch (Exception ex)
        {
            // Log or handle the error as needed
            Console.WriteLine($"Could not open the URL: {ex.Message}");
        }
    }
}
