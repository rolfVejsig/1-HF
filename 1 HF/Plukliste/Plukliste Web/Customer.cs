public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Forsendelse { get; set; }
    public string Type { get; set; }
    public string Produktnr { get; set; }
    public string Navn { get; set; }
    public List<Product> Products { get; set; } // Navigation property
}