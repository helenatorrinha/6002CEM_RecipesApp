using _6002CEM_HelenaTorrinha.Interfaces;
using _6002CEM_HelenaTorrinha.Models;

namespace _6002CEM_HelenaTorrinha
{
    public class AppState : IAppState
    {
        public User CurrentUser { get; set; }

        public AppState()
        {

        }
    }
}



