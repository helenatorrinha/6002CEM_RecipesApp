using System.Diagnostics;
using System.Text.Json;
using _6002CEM_HelenaTorrinha.Dtos;
using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Models;

namespace _6002CEM_HelenaTorrinha.Services
{
    public class QuoteRealService : IQuoteService
    {
        HttpClient _client;
        JsonSerializerOptions? _serializerOptions;

        public QuoteRealService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<QuoteModel?> GetQuote()
        {
            Uri uri = new Uri(string.Concat(Constants.RestEndpointBase, Constants.Endpoints.RandomQuote));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<QuoteModel>(content, _serializerOptions);
                }
                return new QuoteModel();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return new QuoteModel();
            }
        }

        public async Task<List<QuoteModel>> GetQuotes()
        {
            var items = new List<QuoteModel>();
            Uri uri = new Uri(string.Concat(Constants.RestEndpointBase, Constants.Endpoints.ListRandomQuotes));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var dto = JsonSerializer.Deserialize<QuoteServiceGetQuotesResponse>(content, _serializerOptions);
                    return dto.Results;
                }
                return new List<QuoteModel>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return new List<QuoteModel>();
            }

        }

        public Task<bool> SaveQuote(QuoteModel model)
        {
            throw new NotImplementedException();
        }


    }
}


