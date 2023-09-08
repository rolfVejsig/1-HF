using System;

class Front
{
    static void Main()
    {
        string filePath = "database.json";
        DAL dal = new(filePath);
        BLL bll = new(dal);

        dal.Load();

        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddCustomer(bll);
                    break;
                case "2":
                    AddAccount(bll);
                    break;
                case "3":
                    ShowAccounts(bll);
                    break;
                case "4":
                    dal.Save();
                    return;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. Add Account");
        Console.WriteLine("3. Show Accounts");
        Console.WriteLine("4. Save and Exit");
    }

    static void AddCustomer(BLL bll)
    {
        Console.Write("Enter CPR: ");
        string cpr = Console.ReadLine();
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        bll.AddCustomer(new Customer { CPR = cpr, Name = name });
    }

    static void AddAccount(BLL bll)
    {
        Console.Write("Enter CPR: ");
        string cpr = Console.ReadLine();
        Console.Write("Enter Account Number: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Enter Type (Savings/Loan): ");
        string type = Console.ReadLine();
        Console.Write("Enter Balance: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal balance) &&
            float.TryParse(Console.ReadLine(), out float interestRate))
        {
            bll.AddAccount(new Account { AccountNumber = accountNumber, Type = type, Balance = balance, InterestRate = interestRate }, cpr);
        }
        else
        {
            Console.WriteLine("Invalid Balance or Interest Rate.");
        }
    }

    static void ShowAccounts(BLL bll)
    {
        foreach (var customer in bll.GetAllCustomers())
        {
            Console.WriteLine($"Name: {customer.Name}, CPR: {customer.CPR}");
            foreach (var account in customer.Accounts)
            {
                Console.WriteLine($"   AccountNumber: {account.AccountNumber}, Type: {account.Type}, Balance: {account.Balance}, InterestRate: {account.InterestRate}");
            }
        }
    }
}
