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

        public CommentRepository()
        {
            _database = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            _database.CreateTable<Comment>();
        }

        public List<Comment> GetCommentsForPicture(int pictureId)
        {
            var comments = _database.Table<Comment>().Where(c => c.PictureId == pictureId).OrderBy(c => c.CreatedAt).ToList();

            //foreach (var comment in comments)
            //{
            //    // Fetch the username for each comment's UserId
            //    var user = _database.Table<User>().FirstOrDefault(u => u.Id == comment.UserId);
            //    comment = user != null ? user.Username : "Unknown";
            //}

            return comments;
        }


        public void AddComment(int pictureId, int userId, string content)
        {
            var newComment = new Comment
            {
                PictureId = pictureId,
                UserId = userId,
                Text = content,
                CreatedAt = DateTime.Now
            };

            _database.Insert(newComment);
        }



    }
}
