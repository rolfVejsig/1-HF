using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class DAL
{
    private readonly string _filePath;

    public List<Customer> Customers { get; set; } = new();

    public DAL(string filePath) => _filePath = filePath;

    public void Save() => File.WriteAllText(_filePath, JsonConvert.SerializeObject(Customers));

    public void Load()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            Customers = JsonConvert.DeserializeObject<List<Customer>>(json);
        }
    }
}
