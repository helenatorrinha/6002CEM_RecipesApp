using Microsoft.Extensions.Logging;
using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Services;
using _6002CEM_HelenaTorrinha.ViewModels;
using _6002CEM_HelenaTorrinha.Views;

namespace _6002CEM_HelenaTorrinha;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiMicroMvvm<AppShell>(
                "Resources/Styles/Colors.xaml",
                "Resources/Styles/Styles.xaml")
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services
            .MapView<BmiPage, BmiPageViewModel>()
            .MapView<QuoteGeneratorPage, QuoteGeneratorPageViewModel>()
            .MapView<SavedQuotes, SavedQuotesViewModel>()
            .MapView<AppShell, AppShellViewModel>()
            .MapView<LoginPage, LoginPageViewModel>()
            .MapView<RegisterPage, RegisterPageViewModel>();

        builder.Services.AddSingleton<BaseViewModel>();
        builder.Services.AddSingleton<IAppState, AppState>();
        builder.Services.AddSingleton<IQuoteService, QuoteRealService>();
        builder.Services.AddSingleton<IDatabaseService, DatabaseService>();


        return builder.Build();
    }
}