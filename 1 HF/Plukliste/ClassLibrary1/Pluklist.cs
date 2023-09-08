namespace Plukliste
{
    public class Pluklist
    {
        public string? Name { get; set; }
        public string? Forsendelse { get; set; }
        public string? Adresse { get; set; }
        public List<Item> Lines { get; set; } = new List<Item>();

        public Pluklist() { }

        public Pluklist(string? name, string? forsendelse, string? adresse)
        {
            Name = name;
            Forsendelse = forsendelse;
            Adresse = adresse;
        }

        public void AddItem(Item item)
        {
            Lines.Add(item);
        }
    }

    public class Item
    {
        public string ProductID { get; set; }
        public string Title { get; set; }
        public ItemType Type { get; set; }
        public int Amount { get; set; }

        public Item() { }

        public Item(string productId, string title, ItemType type, int amount)
        {
            ProductID = productId;
            Title = title;
            Type = type;
            Amount = amount;
        }
    }

    public enum ItemType
    {
        Fysisk,
        Print
    }
}
