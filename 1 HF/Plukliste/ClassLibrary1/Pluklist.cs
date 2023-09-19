public class Pluklist
{
    public string Name { get; set; }
    public string Forsendelse { get; set; }
    public List<Line> Lines { get; set; }
}

public class Line
{
    public string Amount { get; set; }
    public string Type { get; set; }
    public string ProductID { get; set; }
    public string Title { get; set; }
}
public enum UserOperation
{
    Add,
    Remove,
    ReplaceModem
}
