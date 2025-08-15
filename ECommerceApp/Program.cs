using System;

public class InventoryApp
{
    private InventoryLogger<InventoryItem> _logger = new InventoryLogger<InventoryItem>("inventory.json");

    public void SeedSampleData()
    {
        _logger.Add(new InventoryItem(1, "Laptop", 10, DateTime.Now.AddDays(-10)));
        _logger.Add(new InventoryItem(2, "Smartphone", 25, DateTime.Now.AddDays(-5)));
        _logger.Add(new InventoryItem(3, "Headphones", 50, DateTime.Now.AddDays(-2)));
        _logger.Add(new InventoryItem(4, "Monitor", 15, DateTime.Now.AddDays(-7)));
        _logger.Add(new InventoryItem(5, "Keyboard", 30, DateTime.Now.AddDays(-1)));
    }

    public void SaveData()
    {
        _logger.SaveToFile();
    }

    public void LoadData()
    {
        _logger.LoadFromFile();
    }

    public void PrintAllItems()
    {
        var items = _logger.GetAll();
        foreach (var item in items)
        {
            Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded:d}");
        }
    }

    public void ClearMemory()
    {
        _logger.Clear();
    }

    public static void Main()
    {
        var app = new InventoryApp();
        app.SeedSampleData();
        app.SaveData();
        app.ClearMemory(); // Simulate new session
        app.LoadData();
        Console.WriteLine("All Inventory Items:");
        app.PrintAllItems();
    }
}
