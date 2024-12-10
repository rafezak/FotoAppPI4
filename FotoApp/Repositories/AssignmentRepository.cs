using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FotoApp.Modles;
using FotoApp.JoinTables;
using FotoApp.ViewModels;
using SQLite;

namespace FotoApp.Repositories
{


    public class AssignmentRepository
    {
        private readonly SQLiteConnection _database;

        public AssignmentRepository()
        {
            SQLitePCL.Batteries_V2.Init();
            _database = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);

            _database.CreateTable<Assignment>();
            _database.CreateTable<Theme>();
            _database.CreateTable<AssignmentTheme>();
            _database.CreateTable<AssignmentUser>();
        }

        public List<Assignment> GetAllAssignments() =>
            _database.Table<Assignment>().ToList();

        public Assignment GetAssignmentById(int id) =>
            _database.Find<Assignment>(id);

        public void AddAssignment(Assignment assignment)
        {
            _database.Insert(assignment);
        }

        public int UpdateAssignment(Assignment assignment) =>
            _database.Update(assignment);

        public int DeleteAssignment(int id)
        {
            var assignment = _database.Find<Assignment>(id);
            return assignment != null ? _database.Delete(assignment) : 0;
        }

        // Example usage inside your repository or service layer
        public void AddSampleData()
        {
            var theme = new Theme { Name = "Urgent" };
            _database.Insert(theme);

            var assignment = new Assignment
            {
                Name = "Science Project",
                Description = "Prepare the presentation",

            };
            _database.Insert(assignment);

            // Link the theme to the assignment
            AddThemeToAssignment(assignment.Id, theme.Id);
        }

        





        public void AddThemeToAssignment(int assignmentId, int themeId)
        {
            var assignmentTheme = new AssignmentTheme
            {
                AssignmentId = assignmentId,
                ThemeId = themeId
            };
            _database.Insert(assignmentTheme);
        }

        public List<Theme> GetThemesForAssignment(int assignmentId)
        {
            var themeIds = _database.Table<AssignmentTheme>()
                                    .Where(at => at.AssignmentId == assignmentId)
                                    .Select(at => at.ThemeId)
                                    .ToList();

            return _database.Table<Theme>()
                            .Where(t => themeIds.Contains(t.Id))
                            .ToList();
        }

        public List<Assignment> GetAssignmentsForTheme(int themeId)
        {
            var assignmentIds = _database.Table<AssignmentTheme>()
                                         .Where(at => at.ThemeId == themeId)
                                         .Select(at => at.AssignmentId)
                                         .ToList();

            return _database.Table<Assignment>()
                            .Where(a => assignmentIds.Contains(a.Id))
                            .ToList();
        }

        public List<AssignmentViewModel> GetAssignmentsWithThemes()
        {
            var assignments = _database.Table<Assignment>().ToList();
            var assignmentViewModels = new List<AssignmentViewModel>();

            foreach (var assignment in assignments)
            {
                var themeIds = _database.Table<AssignmentTheme>()
                                        .Where(at => at.AssignmentId == assignment.Id)
                                        .Select(at => at.ThemeId)
                                        .ToList();

                var themes = _database.Table<Theme>()
                                      .Where(t => themeIds.Contains(t.Id))
                                      .ToList();

                assignmentViewModels.Add(new AssignmentViewModel
                {
                    Id = assignment.Id,
                    Name = assignment.Name,
                    Description = assignment.Description,
                    Themes = themes
                });
            }

            return assignmentViewModels;
            
        }

        public List<AssignmentViewModel> GetAssignmentsWithThemesForUser(int userId)
        {
            var assignmentIds = _database.Table<AssignmentUser>()
                                         .Where(au => au.UserId == userId)
                                         .Select(au => au.AssignmentId)
                                         .ToList();

            var assignments = _database.Table<Assignment>()
                                       .Where(a => assignmentIds.Contains(a.Id))
                                       .ToList();

            var assignmentViewModels = new List<AssignmentViewModel>();

            foreach (var assignment in assignments)
            {
                var themeIds = _database.Table<AssignmentTheme>()
                                        .Where(at => at.AssignmentId == assignment.Id)
                                        .Select(at => at.ThemeId)
                                        .ToList();

                var themes = _database.Table<Theme>()
                                      .Where(t => themeIds.Contains(t.Id))
                                      .ToList();

                assignmentViewModels.Add(new AssignmentViewModel
                {
                    Id = assignment.Id,
                    Name = assignment.Name,
                    Description = assignment.Description,
                    Themes = themes
                });
            }

            return assignmentViewModels;
        }



        public void JoinAssignment(int userId, int assignmentId)
        {
            // Check if the user is already joined to the assignment
            var existingJoin = _database.Table<AssignmentUser>()
                                         .FirstOrDefault(au => au.UserId == userId && au.AssignmentId == assignmentId);

            if (existingJoin == null)
            {
                var assignmentUser = new AssignmentUser
                {
                    UserId = userId,
                    AssignmentId = assignmentId
                };

                // Insert into the AssignmentUser join table
                _database.Insert(assignmentUser);
            }
        }


        public List<Assignment> GetAssignmentsForUser(int userId)
        {
            var userAssignments = _database.Table<AssignmentUser>()
                                            .Where(au => au.UserId == userId)
                                            .ToList();

            var assignments = new List<Assignment>();
            foreach (var userAssignment in userAssignments)
            {
                var assignment = _database.Table<Assignment>()
                                           .FirstOrDefault(a => a.Id == userAssignment.AssignmentId);
                if (assignment != null)
                {
                    assignments.Add(assignment);
                }
            }

            return assignments;
        }


        public bool HasUserJoinedAssignment(int userId, int assignmentId)
        {
            return _database.Table<AssignmentUser>()
                            .Any(au => au.UserId == userId && au.AssignmentId == assignmentId);
        }








    }
}
