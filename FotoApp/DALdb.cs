using FotoApp.Modles;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


// KEY 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

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
