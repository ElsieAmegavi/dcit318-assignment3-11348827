using System;
using System.Collections.Generic;

public class WareHouseManager
{
    private InventoryRepository<ElectronicItem> _electronics = new InventoryRepository<ElectronicItem>();
    private InventoryRepository<GroceryItem> _groceries = new InventoryRepository<GroceryItem>();

    public void SeedData()
    {
        // Electronics
        _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Dell", 24));
        _electronics.AddItem(new ElectronicItem(2, "Smartphone", 20, "Samsung", 12));
        _electronics.AddItem(new ElectronicItem(3, "Headphones", 15, "Sony", 18));
        // Groceries
        _groceries.AddItem(new GroceryItem(1, "Milk", 30, DateTime.Now.AddDays(7)));
        _groceries.AddItem(new GroceryItem(2, "Bread", 50, DateTime.Now.AddDays(3)));
        _groceries.AddItem(new GroceryItem(3, "Eggs", 100, DateTime.Now.AddDays(14)));
    }

    public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
    {
        foreach (var item in repo.GetAllItems())
        {
            Console.WriteLine(item is ElectronicItem e
                ? $"[Electronic] ID: {e.Id}, Name: {e.Name}, Brand: {e.Brand}, Warranty: {e.WarrantyMonths} months, Qty: {e.Quantity}"
                : item is GroceryItem g
                    ? $"[Grocery] ID: {g.Id}, Name: {g.Name}, Expiry: {g.ExpiryDate:d}, Qty: {g.Quantity}"
                    : $"ID: {item.Id}, Name: {item.Name}, Qty: {item.Quantity}");
        }
    }

    public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
    {
        try
        {
            var item = repo.GetItemById(id);
            repo.UpdateQuantity(id, item.Quantity + quantity);
            Console.WriteLine($"Stock increased for item ID {id}. New quantity: {item.Quantity + quantity}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error increasing stock: {ex.Message}");
        }
    }

    public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
    {
        try
        {
            repo.RemoveItem(id);
            Console.WriteLine($"Item with ID {id} removed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing item: {ex.Message}");
        }
    }

    public static void Main()
    {
        var manager = new WareHouseManager();
        manager.SeedData();
        Console.WriteLine("All Grocery Items:");
        manager.PrintAllItems(manager._groceries);
        Console.WriteLine("\nAll Electronic Items:");
        manager.PrintAllItems(manager._electronics);

        // Try to add duplicate item
        try
        {
            manager._groceries.AddItem(new GroceryItem(1, "Butter", 20, DateTime.Now.AddDays(10)));
        }
        catch (DuplicateItemException ex)
        {
            Console.WriteLine($"Duplicate error: {ex.Message}");
        }

        // Try to remove non-existent item
        manager.RemoveItemById(manager._electronics, 99);

        // Try to update with invalid quantity
        try
        {
            manager._groceries.UpdateQuantity(2, -5);
        }
        catch (InvalidQuantityException ex)
        {
            Console.WriteLine($"Invalid quantity error: {ex.Message}");
        }
    }
}
