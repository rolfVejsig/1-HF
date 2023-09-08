using System.Collections.Generic;

public class Account
{
    public string AccountNumber { get; init; }
    public string Type { get; init; }
    public decimal Balance { get; init; }
    public float InterestRate { get; init; }
}

public class Customer
{
    public string CPR { get; init; }
    public string Name { get; init; }
    public List<Account> Accounts { get; init; } = new();
}
