using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FotoApp.Modles;  

namespace FotoApp.Repositories
{
    class CommentRepository
    {
        private readonly SQLiteConnection _database;

        public CommentRepository(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
        }

        public List<Comment> GetCommentsByPicture(int pictureId)
        {
            return _database.Table<Comment>().Where(c => c.PictureId == pictureId).ToList();
        }

        public void AddComment(Comment comment)
        {
            _database.Insert(comment);
        }
    }
}
