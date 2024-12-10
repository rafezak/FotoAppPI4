using FotoApp.JoinTables;
using FotoApp.Modles;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);

        _database.ExecuteAsync("PRAGMA foreign_keys = ON;").Wait();

        _database.CreateTableAsync<User>().Wait();
        _database.CreateTableAsync<Assignment>().Wait();
        _database.CreateTableAsync<Picture>().Wait();
        _database.CreateTableAsync<Comment>().Wait();
        _database.CreateTableAsync<AssignmentUser>().Wait();

    }

    public Task<List<Assignment>> GetItemsAsync()
    {
        return _database.Table<Assignment>().ToListAsync();
    }

    public Task<Assignment?> GetItemAsync(int id)
    {
        return _database.Table<Assignment>().FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<bool> AssignUserToAssignment(int userId, int assignmentId)
    {
        var assignmentExists = await _database.Table<Assignment>().FirstOrDefaultAsync(a => a.Id == assignmentId);
        var userExists = await _database.Table<User>().FirstOrDefaultAsync(u => u.Id == userId);

        if (assignmentExists == null || userExists == null)
        {
            return false; // Either the assignment or user does not exist
        }

        var assignmentUser = new AssignmentUser
        {
            UserId = userId,
            AssignmentId = assignmentId,
            
        };

        await _database.InsertAsync(assignmentUser);
        return true;
    }

    public Task<int> SaveItemAsync(Assignment item)
    {
        if (item.Id != 0)
        {
            return _database.UpdateAsync(item);
        }
        else
        {
            return _database.InsertAsync(item);
        }
    }

    public Task<int> DeleteItemAsync(int id)
    {
        return _database.DeleteAsync<Assignment>(id);
    }
}
