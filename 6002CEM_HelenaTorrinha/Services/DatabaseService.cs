using SQLite;
using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Models;

namespace _6002CEM_HelenaTorrinha.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            _database.CreateTableAsync<QuoteModel>();
        }

        public Task<int> EditQuote(QuoteModel quoteModel)
        {
            //To be implemented in class
            return Task.FromResult(1);
        }

        public async Task<List<QuoteModel>> GetQuotes()
        {
            try
            {
                var res = await _database.Table<QuoteModel>().ToListAsync();
                return res;
            }
            catch (Exception e)
            {
                return new List<QuoteModel>();
            }
        }

        public async Task<int> RemoveQuote(string quoteModelId)
        {
            try
            {
                var res = await _database.Table<QuoteModel>().DeleteAsync(x => x.Id.Equals(quoteModelId));
                return res;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<int> SaveQuote(QuoteModel quoteModel)
        {
            try
            {
                var res = await _database.InsertAsync(quoteModel);
                return res;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
    }
}