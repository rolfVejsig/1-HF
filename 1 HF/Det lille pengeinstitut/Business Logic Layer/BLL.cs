using System.Collections.Generic;

public class BLL
{
    private readonly DAL _dal;

    public BLL(DAL dal) => _dal = dal;

    public void AddCustomer(Customer customer) => _dal.Customers.Add(customer);

    public void AddAccount(Account account, string cpr)
    {
        var customer = _dal.Customers.Find(c => c.CPR == cpr);
        customer?.Accounts.Add(account);
    }

    public List<Customer> GetAllCustomers() => _dal.Customers;
}
