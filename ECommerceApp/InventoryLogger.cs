using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class InventoryLogger<T> where T : IInventoryEntity
{
    private List<T> _log = new List<T>();
    private readonly string _filePath;

    public InventoryLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Add(T item)
    {
        _log.Add(item);
    }

    public List<T> GetAll()
    {
        return new List<T>(_log);
    }

    public void SaveToFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(_log, new JsonSerializerOptions { WriteIndented = true });
            using (var writer = new StreamWriter(_filePath))
            {
                writer.Write(json);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving to file: {ex.Message}");
        }
    }

    public void LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("File not found. No data loaded.");
                return;
            }
            using (var reader = new StreamReader(_filePath))
            {
                var json = reader.ReadToEnd();
                var items = JsonSerializer.Deserialize<List<T>>(json);
                _log = items ?? new List<T>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading from file: {ex.Message}");
        }
    }

    public void Clear()
    {
        _log.Clear();
    }
}
