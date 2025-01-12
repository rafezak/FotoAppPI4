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

        public PictureRepository()
        {
            _database = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            _database.CreateTable<Picture>();
        }

        // Add a picture to the database
        public void AddPicture(Picture picture)
        {
            _database.Insert(picture);
        }

        // Get all pictures for a specific assignment
        public List<Picture> GetPicturesForAssignment(int assignmentId)
        {
            return _database.Table<Picture>()
                            .Where(p => p.AssignmentId == assignmentId)
                            .ToList();
        }

        // Get a picture for a specific assignment and user
        public Picture GetPictureForAssignmentAndUser(int assignmentId, int userId)
        {
            return _database.Table<Picture>()
                            .FirstOrDefault(p => p.AssignmentId == assignmentId && p.UploadedBy == userId);
        }

        // Like a picture by incrementing the likes count
        public void LikePicture(int pictureId)
        {
            var picture = _database.Table<Picture>().FirstOrDefault(p => p.Id == pictureId);
            if (picture != null)
            {
                picture.Likes++;
                _database.Update(picture);
            }
        }

    }
}
