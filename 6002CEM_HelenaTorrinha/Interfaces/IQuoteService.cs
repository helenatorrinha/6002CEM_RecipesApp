using _6002CEM_HelenaTorrinha.Models;

namespace _6002CEM_HelenaTorrinha.Interfaces
{
    public interface IQuoteService
    {
        public Task<List<Models.QuoteModel>> GetQuotes();
        public Task<QuoteModel?> GetQuote();
        public Task<bool> SaveQuote(QuoteModel model);

    }
}

