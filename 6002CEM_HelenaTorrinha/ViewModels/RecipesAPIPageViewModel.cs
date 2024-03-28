using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using Supabase;
using _6002CEM_HelenaTorrinha.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Newtonsoft.Json;


namespace _6002CEM_HelenaTorrinha.ViewModels;

public class RecipesAPIPageViewModel : BaseViewModel
{
    private readonly Client _supabaseClient;
    public ObservableCollection<RecipeAPI> Recipes { get; set; } = new ObservableCollection<RecipeAPI>();
    private readonly HttpClient _httpClient = new HttpClient();

    public RecipeAPI recipe { get; set; }
    public string title { get; set; }
    public int readyInMinutes { get; set; }
    public int servings { get; set; }
    public string ingredients { get; set; }
    public string sourceUrl { get; set; }
    public string image { get; set; }

    public ICommand NavigateToMyRecipesPageCommand { get; set; }

    public ICommand ItemTappedCommand { get; private set; }

    public RecipesAPIPageViewModel(ViewModelContext context, IAppState appState) : base(context)
    {
        _supabaseClient = new Client(SupabaseDetails.Url, SupabaseDetails.SupabaseKey);
        Task.Run(() => GetPage());
        ItemTappedCommand = new Command<RecipeAPI>(OnItemTapped);
    }

    public override void OnAppearing()
    {
        base.OnAppearing();
    }

    private async Task GetPage()
    {
        try
        {
            var apiKey = SpoonacularDetails.SpoonacularKey; //The api key

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spoonacular.com/recipes/random?apiKey={apiKey}&number=10"); 

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response
            var recipesResponse = JsonConvert.DeserializeObject<SpoonacularRandomRecipesResponse>(jsonResponse);

            // Update the ObservableCollection with the new data
            Recipes.Clear();
            if (recipesResponse != null && recipesResponse.Recipes != null)
            {
                foreach (var recipe in recipesResponse.Recipes)
                {
                    Recipes.Add(new RecipeAPI
                    {
                        // Map the properties from the API response to the RecipeDatabase model
                        title = recipe.Title,
                        readyInMinutes = recipe.ReadyInMinutes, 
                        servings = recipe.Servings,
                        ingredients = string.Join(", ", recipe.extendedIngredients.Select(ing => ing.original)), // Example, adjust as needed
                        sourceUrl = recipe.sourceUrl,
                        image = recipe.image
                    });
                }
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

    // Example classes for deserialization (adjust according to the actual JSON structure)
    public class SpoonacularRandomRecipesResponse
    {
        [JsonProperty("recipes")]
        public List<Recipe> Recipes { get; set; }
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

    private async void OnItemTapped(RecipeAPI item)
    {
        if (!string.IsNullOrWhiteSpace(item.sourceUrl))
        {
            try
            {
                await Launcher.OpenAsync(new Uri(item.sourceUrl));
            }
            catch (Exception ex)
            {
                // Log or handle the error as needed
                Console.WriteLine($"Could not open the URL: {ex.Message}");
            }
        }
    }

}
