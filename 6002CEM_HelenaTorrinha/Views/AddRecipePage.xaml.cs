using _6002CEM_HelenaTorrinha.ViewModels;

namespace _6002CEM_HelenaTorrinha.Views;

public partial class AddRecipe
{
	public AddRecipe()
	{
		InitializeComponent();
	}

    void Name_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.AddRecipePageViewModel);
        viewModel.Name = e.NewTextValue;
    }

    void PrepTime_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        if (BindingContext is ViewModels.AddRecipePageViewModel viewModel)
        {
            if (int.TryParse(e.NewTextValue, out var prepTime))
            {
                viewModel.Preptime = prepTime;
            }
           
        }
    }

    void CookTime_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        if (BindingContext is ViewModels.AddRecipePageViewModel viewModel)
        {
            if (int.TryParse(e.NewTextValue, out var cookTime))
            {
                viewModel.Cooktime = cookTime;
            }
        }
    }

    void Servings_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        if (BindingContext is ViewModels.AddRecipePageViewModel viewModel)
        {
            if (int.TryParse(e.NewTextValue, out var servings))
            {
                viewModel.Servings = servings;
            }
        }
    }
    void Ingredients_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.AddRecipePageViewModel);
        viewModel.Ingredients = e.NewTextValue;
    }

    void Instructions_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.AddRecipePageViewModel);
        viewModel.Instructions = e.NewTextValue;
    }
}
