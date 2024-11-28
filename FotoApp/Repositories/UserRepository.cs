using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FotoApp.Modles;
using BCrypt.Net;

namespace FotoApp.Repositories
{
    public class UserRepository
    {
        private readonly SQLiteConnection _database;

        public UserRepository()
        {
            _database = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            _database.CreateTable<User>();
        }

        public bool RegisterUser(string username, string password, string role = "user")
        {
            // Check if user exists
            if (_database.Table<User>().Any(u => u.Username == username))
            {
                return false; // User already exists
            }

            // Hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Create and insert user
            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Role = role
            };
            _database.Insert(user);

            return true;
        }

        public User LoginUser(string username, string password)
        {
            var user = _database.Table<User>().FirstOrDefault(u => u.Username == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user; // Login successful
            }
            return null; // Login failed
        }
    }

}
