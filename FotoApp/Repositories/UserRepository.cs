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

            if (!_database.Table<User>().Any(u => u.Role == UserRoles.Admin))
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword("admin");
                var Admin = new User
                {
                    Username = "admin",
                    PasswordHash = hashedPassword,
                    Role = UserRoles.Admin,
                    Points = 10

                };

                _database.Insert(Admin);
            }
        }

        public bool RegisterUser(string username, string password, string role = UserRoles.Member)
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

        public string GetUserRole(string username)
        {
            var user = _database.Table<User>().FirstOrDefault(u => u.Username == username);
            return user?.Role;
        }


        public List<User> GetAllUsers() => _database.Table<User>().ToList();

        public User GetUserById(int userId) => _database.Find<User>(userId);



        public void UpdateUserPoints(int userId, int points)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                user.Points = points;
                _database.Update(user);
            }
        }


        public void DeleteUser(int userId)
        {
            _database.Delete<User>(userId);
        }

        public void UpdateUser(User user)
        {
            // Update the user's record in the database
            _database.Update(user);  // This assumes User has a primary key and will be updated based on the Id
        }
    }


}
