using FotoApp.Modles;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoApp.Repositories
{
    class PictureRepository
    {
        private readonly SQLiteConnection _database;

        public PictureRepository(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
        }

        public List<Picture> GetPicturesByAssignment(int assignmentId)
        {
            return _database.Table<Picture>().Where(p => p.AssignmentId == assignmentId).ToList();
        }

        public void AddPicture(Picture picture)
        {
            _database.Insert(picture);
        }
    }
}
