using Postgrest.Attributes;
using Postgrest.Models;

namespace _6002CEM_HelenaTorrinha.Models
{
    public class RecipeAPI
    {
        public int id { get; set; }
        public string title { get; set; }
        public int readyInMinutes { get; set; }
        public int servings { get; set; }
        public string ingredients { get; set; }
        public string sourceUrl { get; set; }
        public string image { get; set; }
    }

}

