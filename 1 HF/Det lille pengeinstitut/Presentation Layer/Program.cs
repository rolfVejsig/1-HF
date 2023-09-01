using System;
using System.Security.Principal;

class Program
{
    static void Main()
    {
        DAL dal = new DAL();
        BLL bll = new BLL(dal);

        // Load data from JSON file
        dal.LoadFromJSON();

        while (true)
        {
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. Add Account");
            Console.WriteLine("3. Show Accounts");
            Console.WriteLine("4. Save and Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter CPR: ");
                    string cpr = Console.ReadLine();
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    bll.AddCustomer(new Customer { CPR = cpr, Name = name });
                    break;

                case "2":
                    Console.Write("Enter CPR: ");
                    string accountCPR = Console.ReadLine();
                    Console.Write("Enter Account Number: ");
                    string accountNumber = Console.ReadLine();
                    Console.Write("Enter Type (Savings/Loan): ");
                    string type = Console.ReadLine();
                    Console.Write("Enter Balance: ");
                    decimal balance = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter Interest Rate: ");
                    float interestRate = float.Parse(Console.ReadLine());
                    bll.AddAccount(new Account { AccountNumber = accountNumber, Type = type, Balance = balance, InterestRate = interestRate }, accountCPR);
                    break;

                case "3":
                    foreach (var customer in bll.GetAllCustomers())
                    {
                        Console.WriteLine($"Name: {customer.Name}, CPR: {customer.CPR}");
                        foreach (var account in customer.Accounts)
                        {
                            Console.WriteLine($"   AccountNumber: {account.AccountNumber}, Type: {account.Type}, Balance: {account.Balance}, InterestRate: {account.InterestRate}");
                        }
                    }
                    break;

                case "4":
                    // Save data to JSON file and exit
                    dal.SaveToJSON();
                    return;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }
}
