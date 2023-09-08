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

/*
 Æmdringer i forhold til den originale kode:

 Properties: Jeg har omdannet de offentlige felter til properties. Dette er mere i tråd med C#-best practice og tillader nemmere datamanipulation og -validering.

 Constructors: Jeg har tilføjet parameteriserede constructors for Pluklist og Item klasser, som gør det muligt at initialisere objekter med bestemte værdier.

 Initialisering af List: Jeg har initialiseret Lines listen direkte i property-definitionen, hvilket er mere i tråd med C# 6.0+ syntaks. 

 Tom Constructor: En tom constructor er tilføjet til både Pluklist og Item klasserne, hvilket er nyttigt, når objekter skal deserialiseres fra XML eller andre dataformater.
*/