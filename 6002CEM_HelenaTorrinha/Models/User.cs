using System.Runtime.CompilerServices;

namespace _6002CEM_HelenaTorrinha.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }

        private static User _instance;

        public User()
        {
        }

        // Singleton pattern
        public static User Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new User();
                }
                return _instance;
            }
        }

        public static void Clear()
        {
            _instance = null;
        }
    }
}