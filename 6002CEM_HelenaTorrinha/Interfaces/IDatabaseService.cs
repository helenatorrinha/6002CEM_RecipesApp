using _6002CEM_HelenaTorrinha.Models;

namespace _6002CEM_HelenaTorrinha.Interfaces
{
    public interface IDatabaseService
    {
        Task<List<QuoteModel>> GetQuotes();
        Task<int> SaveQuote(QuoteModel quoteModel);
        Task<int> EditQuote(QuoteModel quoteModel);
        Task<int> RemoveQuote(string quoteModelId);
    }
}

