using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiMicroMvvm;
using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Models;

namespace _6002CEM_HelenaTorrinha.ViewModels;

public class SavedQuotesViewModel : BaseViewModel
{
    private readonly IDatabaseService _database;

    public ObservableCollection<QuoteModel> Quotes { get; set; } = new();
    public ICommand DeleteCommand { get; set; }


    public bool ShouldShowDefaultText
    {
        get => Get<bool>();
        set => Set(value);
    }
    public bool ShouldShowPopulatedLayout
    {
        get => Get<bool>();
        set => Set(value);
    }

    public SavedQuotesViewModel(ViewModelContext context, IDatabaseService database) : base(context)
    {
        this._database = database;
        ShouldShowDefaultText = true;
        ShouldShowPopulatedLayout = false;
        DeleteCommand = new Command<QuoteModel>(async (quote) => await DeleteQuote(quote));

    }

    public override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            await FetchAllSavedQuotes();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private async Task FetchAllSavedQuotes()
    {
        var listOfQuotes = await _database.GetQuotes();
        listOfQuotes.ForEach((quote) =>
        {
            var isDuplicate = Quotes.FirstOrDefault(q => q.Id == quote.Id);
            if (isDuplicate == null)
            {
                Quotes.Add(quote);
            }
        });

        if (Quotes.Count > 0)
        {
            ShouldShowDefaultText = false;
            ShouldShowPopulatedLayout = true;
        }
        else
        {
            ShouldShowDefaultText = true;
            ShouldShowPopulatedLayout = false;
        }
    }

    private async Task DeleteQuote(QuoteModel quoteToDelete)
    {
        var result = await _database.RemoveQuote(quoteToDelete.Id.ToString());

        if (result > 0)
        {
            Quotes.Remove(quoteToDelete);
            await Shell.Current.DisplayAlert("Success", "Item has been removed", "OK");

            if (Quotes.Count == 0)
            {
                ShouldShowDefaultText = true;
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Something went wrong. Please try again", "OK");

        }
    }
}