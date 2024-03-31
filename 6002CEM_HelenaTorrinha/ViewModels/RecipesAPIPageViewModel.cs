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
    public ObservableCollection<RecipeAPI> Recipes { get; set; } = new ObservableCollection<RecipeAPI>();
    private readonly HttpClient _httpClient = new HttpClient();

    public RecipeAPI recipe { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string image { get; set; }

    public ICommand NavigateToMyRecipesPageCommand { get; set; }

    public ICommand ItemTappedCommand { get; private set; }

    public RecipeAPI selectedRecipe { get; set; }

    public RecipesAPIPageViewModel(ViewModelContext context, IAppState appState) : base(context)
    {
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
                        // Map the properties from the API response to the RecipeAPI model
                        id = recipe.id,
                        title = recipe.Title,
                        image = recipe.image
                    });
                }
            }

            Recipes = new ObservableCollection<RecipeAPI>(Recipes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching recipes: {ex.Message}");
        }
    }

    public class SpoonacularRandomRecipesResponse
    {
        [JsonProperty("recipes")]
        public List<Recipe> Recipes { get; set; }
    }

    public class Recipe
    {
        // Define properties according to Spoonacular's JSON response
        public int id { get; set; }
        public string Title { get; set; }
        public string image { get; set; }
    }

    public class Ingredient
    {
        public string original { get; set; }
    }

    private async void OnItemTapped(RecipeAPI item)
    {
        await Shell.Current.GoToAsync($"/SingleRecipeAPIPage?id={item.id}");
    }

}
