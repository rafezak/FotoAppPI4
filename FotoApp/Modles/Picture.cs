using SQLite;

namespace FotoApp.Modles
{
   public class Picture
   {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int AssignmentId { get; set; }  // Link to the Assignment

        public DateTime UploadedAt { get; set; }  // When the picture was uploaded

        public string ImagePath { get; set; }  // Path to the image

        public int UploadedBy { get; set; }  // Link to the User

        public int Likes { get; set; }  // Points for the picture  








   }
}
