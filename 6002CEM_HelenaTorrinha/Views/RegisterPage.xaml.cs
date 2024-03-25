namespace _6002CEM_HelenaTorrinha.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
    }

    void Username_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.RegisterPageViewModel);
        viewModel.Username = e.NewTextValue;
    }

    void firstName_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.RegisterPageViewModel);
        viewModel.firstName = e.NewTextValue;
    }

    void lastName_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.RegisterPageViewModel);
        viewModel.lastName = e.NewTextValue;
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

    void ConfirmPassword_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        var viewModel = (BindingContext as ViewModels.RegisterPageViewModel);
        viewModel.ConfirmPassword = e.NewTextValue;
    }
}
