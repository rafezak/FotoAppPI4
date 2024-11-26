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
        _database.CreateTableAsync<Assignment>().Wait();
    }

    public Task<List<Assignment>> GetItemsAsync()
    {
        return _database.Table<Assignment>().ToListAsync();
    }

    public Task<Assignment?> GetItemAsync(int id)
    {
        return _database.Table<Assignment>().FirstOrDefaultAsync(i => i.Id == id);
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
