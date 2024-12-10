using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoApp.Modles
{
    class Picture
    {
        
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public int AssignmentId { get; set; }  // Link to the Assignment

            public DateTime UploadedAt { get; set; }  // When the picture was uploaded

            public string ImagePath { get; set; }  // Path to the image
            
            



      
        

    }
}
