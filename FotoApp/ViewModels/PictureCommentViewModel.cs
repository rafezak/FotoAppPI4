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
        public ObservableCollection<Comment> Comments { get; set; }

        public PictureCommentViewModel(Picture picture, List<Comment> comments)
        {
            Picture = picture;
            Comments = new ObservableCollection<Comment>(comments); // Convert List to ObservableCollection        }
        }





    }
}
