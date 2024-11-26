using FotoApp.Modles;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public class DAL
{
    private readonly DatabaseService _databaseService;

    public ObservableCollection<Assignment> Assignments { get; set; } = new ObservableCollection<Assignment>();

    public DAL(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public async Task LoadItemsAsync()


    {
        var dBService = new DatabaseService("MyAppDatabase.db");
        var items = await _databaseService.GetItemsAsync();
        Assignments.Clear();
        foreach (var item in items)
        {
            Assignments.Add(item);
        }
    }

    public async Task AddItemAsync(string name, string description, decimal price)
    {
        var item = new Assignment { Name = name, Description = description};
        await _databaseService.SaveItemAsync(item);
        await LoadItemsAsync();
    }

    public async Task DeleteItemAsync(int id)
    {
        await _databaseService.DeleteItemAsync(id);
        await LoadItemsAsync();
    }
}
