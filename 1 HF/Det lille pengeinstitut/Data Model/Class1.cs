using System.Collections.Generic;

public class Account
{
    public string AccountNumber { get; set; }
    public string Type { get; set; }
    public decimal Balance { get; set; }
    public float InterestRate { get; set; }
}

public class Customer
{
    public string CPR { get; set; }
    public string Name { get; set; }
    public List<Account> Accounts { get; set; } = new List<Account>();
}
