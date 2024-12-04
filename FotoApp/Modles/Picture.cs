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

            public string ImagePath { get; set; }  // Path to the image

        [ManyToOne]
              // Link to the Assignment (assuming many-to-one relationship)
            public Assignment Assignment { get; set; }
        

    }
}
