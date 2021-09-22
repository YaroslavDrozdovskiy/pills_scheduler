using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace pills_scheduler
{
    public static class Constants
    {
        public const string DatabaseFilename = "PillsSQLite.db3";
        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        public static string DataBasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
    
}
