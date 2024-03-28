namespace _6002CEM_HelenaTorrinha
{
    public static class Constants
    {
        public static string RestEndpointBase = "https://api.quotable.io";

        public const SQLite.SQLiteOpenFlags Flags =
              // open the database in read/write mode
              SQLite.SQLiteOpenFlags.ReadWrite |
              // create the database if it doesn't exist
              SQLite.SQLiteOpenFlags.Create |
              // enable multi-threaded database access
              SQLite.SQLiteOpenFlags.SharedCache;

        public const string DatabaseFilename = "DemoDatabase.db3";

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}