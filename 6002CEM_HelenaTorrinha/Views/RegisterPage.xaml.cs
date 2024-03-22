namespace _6002CEM_HelenaTorrinha.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
    }

    void Email_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.RegisterPageViewModel);
        viewModel.Email = e.NewTextValue;
    }

    void Password_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.RegisterPageViewModel);
        viewModel.Password = e.NewTextValue;
    }
}
