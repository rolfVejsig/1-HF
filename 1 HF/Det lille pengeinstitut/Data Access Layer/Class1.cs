using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class DAL
{
    public List<Customer> Customers { get; set; } = new List<Customer>();

    public void SaveToJSON()
    {
        string json = JsonConvert.SerializeObject(Customers);
        File.WriteAllText("database.json", json);
    }

    public void LoadFromJSON()
    {
        if (File.Exists("database.json"))
        {
            string json = File.ReadAllText("database.json");
            Customers = JsonConvert.DeserializeObject<List<Customer>>(json);
        }
    }
}
