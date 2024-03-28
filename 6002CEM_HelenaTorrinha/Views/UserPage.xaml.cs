using _6002CEM_HelenaTorrinha.ViewModels;

namespace _6002CEM_HelenaTorrinha.Views;

public partial class UserPage
{
    public UserPage()
    {
        InitializeComponent();
    }

    void CurrentPassword_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.UserPageViewModel);
        viewModel.CurrentPassword = e.NewTextValue;
    }

    void NewPassword_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.UserPageViewModel);
        viewModel.NewPassword = e.NewTextValue;
    }
}
