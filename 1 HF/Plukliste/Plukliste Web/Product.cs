public class Product
{
    public int Id { get; set; }
    public int Antal { get; set; }
    public string Type { get; set; }
    public string Produktnr { get; set; }
    public string Navn { get; set; }
    public int CustomerId { get; set; } // Foreign key referencing Customer
    public Customer Customer { get; set; } // Navigation property
}
