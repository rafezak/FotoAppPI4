using FotoApp.Modles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FotoApp.ViewModels
{
    public class PictureCommentViewModel
    {
        public Picture Picture { get; set; }
        public ObservableCollection<CommentViewModel> Comments { get; set; }

        public PictureCommentViewModel(Picture picture, List<Comment> comments, List<User> users)
        {
            Picture = picture;

            // Populate comments with usernames
            Comments = new ObservableCollection<CommentViewModel>(
                comments.Select(comment =>
                {
                    var user = users.FirstOrDefault(u => u.Id == comment.UserId);
                    return new CommentViewModel
                    {
                        Text = comment.Text,
                        Username = user != null ? user.Username : "Unknown"
                    };
                }));
        }
    }
        public class CommentViewModel
        {
            public string Text { get; set; }
            public string Username { get; set; }
        }





    
}
