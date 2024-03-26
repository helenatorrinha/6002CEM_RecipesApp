using System.Windows.Input;
using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Models;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class QuoteGeneratorPageViewModel : BaseViewModel
{
    private readonly IQuoteService _quoteService;
    private readonly IDatabaseService _databaseService;
    private QuoteModel LastSavedQuote;

    public ICommand GenerateNewQuoteCommand { get; set; }
    public ICommand SaveQuoteCommand { get; set; }

    public QuoteModel? CurrentQuote
    {
        get => Get<QuoteModel>();
        set
        {
            Set(value);
            (SaveQuoteCommand as Command).ChangeCanExecute();
        }
    }

    public QuoteGeneratorPageViewModel(ViewModelContext context, IQuoteService quoteService, IDatabaseService databaseService) : base(context)
    {
        _quoteService = quoteService;
        _databaseService = databaseService;
        GenerateNewQuoteCommand = new Command(async () => await GenerateQuoteAsync());
        SaveQuoteCommand = new Command(async () => await SaveQuote(), () => CurrentQuote != null);
    }

    private async Task GenerateQuoteAsync()
    {
        CurrentQuote = await _quoteService.GetQuote();
    }

    private async Task SaveQuote()
    {
        if ((bool)(CurrentQuote?.Id.Equals(LastSavedQuote?.Id)))
        {
            await Shell.Current.DisplayAlert("Oops", "Seems like this quote has already been saved", "OK");
        }
        else
        {
            var result = await _databaseService.SaveQuote(CurrentQuote);
            if (result != 0)
            {
                await Shell.Current.DisplayAlert("Saved", "Quote has been saved", "OK");
                LastSavedQuote = CurrentQuote;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong. Please try again", "OK");
            }
        }
    }

}