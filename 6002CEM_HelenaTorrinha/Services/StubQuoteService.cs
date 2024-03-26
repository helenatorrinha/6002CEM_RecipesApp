using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Models;

namespace _6002CEM_HelenaTorrinha.Services
{
    public class StubQuoteService : IQuoteService
    {
        public List<QuoteModel> Quotes = new()
        {
            new("Sophocles", "Quick decisions are unsafe decisions."),
            new("Louise Hay", "The thoughts we choose to think are the tools we use to paint the canvas of our lives."),
            new("Bernice Reagon",
                "Life's challenges are not supposed to paralyze you, they're supposed to help you discover who you are."),
            new("Pablo Picasso", "Only put off until tomorrow what you are willing to die having left undone."),
            new("Rita Mae Brown",
                "Creativity comes from trust. Trust your instincts. And never hope more than you work."),
            new("Elizabeth Arden",
                "I'm not interested in age. People who tell me their age are silly. You're as old as you feel.")
        };
        public StubQuoteService()
        {
        }

        public Task<QuoteModel?> GetQuote()
        {
            return Task.FromResult(Quotes.OrderBy(x => Guid.NewGuid()).FirstOrDefault());
        }

        public Task<List<QuoteModel>> GetQuotes()
        {
            return Task.FromResult(new List<QuoteModel>()
            {
                new("Sophocles","Quick decisions are unsafe decisions."),
                new("Louise Hay","The thoughts we choose to think are the tools we use to paint the canvas of our lives."),
                new("Bernice Reagon","Life's challenges are not supposed to paralyze you, they're supposed to help you discover who you are."),
                new("Pablo Picasso","Only put off until tomorrow what you are willing to die having left undone."),
                new("Rita Mae Brown","Creativity comes from trust. Trust your instincts. And never hope more than you work."),
                new("Elizabeth Arden","I'm not interested in age. People who tell me their age are silly. You're as old as you feel.")
            });
        }

        public Task<bool> RemoveQuote(int id)
        {
            return Task.FromResult(true);
        }

        public Task<bool> SaveQuote(QuoteModel model)
        {
            return Task.FromResult(true);
        }

        public Task<bool> UpdateQuote(int id, QuoteModel model)
        {
            return Task.FromResult(true);
        }
    }
}
