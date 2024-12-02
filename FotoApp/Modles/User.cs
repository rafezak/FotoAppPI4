using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotoApp.Modles
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Username { get; set; }

        public string PasswordHash { get; set; } // Store hashed password

        public string Role { get; set; } // e.g., "user", "admin"

        public int Points { get; set; } // User points
    }

}
