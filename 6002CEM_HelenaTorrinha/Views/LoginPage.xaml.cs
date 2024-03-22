namespace _6002CEM_HelenaTorrinha.Views;

public partial class LoginPage
{
    public LoginPage()
    {
        InitializeComponent();
     }

    void Email_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.LoginPageViewModel);
        viewModel.Email = e.NewTextValue;
    }

    void Password_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.LoginPageViewModel);
        viewModel.Password = e.NewTextValue;
    }
}
