using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FotoApp.Modles;

namespace FotoApp.Repositories
{
    public class ThemeRepository
    {
        private readonly SQLiteConnection _database;

        public ThemeRepository()
        {
            SQLitePCL.Batteries_V2.Init();
            _database = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);

            _database.CreateTable<Theme>();
        }

        public void AddTheme(Theme theme)
        {
            if (theme == null)
                throw new ArgumentNullException(nameof(theme));

            _database.Insert(theme);
        }

        // Get all themes
        public List<Theme> GetAllThemes()
        {
            return _database.Table<Theme>().ToList();
        }
    }


}
