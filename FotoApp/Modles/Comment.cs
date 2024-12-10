using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoApp.Modles
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
       
        public int PictureId { get; set; }

       
        

    }
}
