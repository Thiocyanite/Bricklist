using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace zadanie2ubi
{
    public class Backend
    {
        private List<String> BrickSetsNames;
        private SQLiteConnection db;

        private static readonly Lazy<Backend>
        lazy =
        new Lazy<Backend>
            (() => new Backend());

        public static Backend Instance { get { return lazy.Value; } }

        private Backend()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "database.db3");
            db = new SQLiteConnection(dbPath);

        }



    }
}
