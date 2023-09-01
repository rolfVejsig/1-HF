using System.Collections.Generic;
using System.Security.Principal;

public class BLL
{
    private DAL _dal;

    public BLL(DAL dal)
    {
        _dal = dal;
    }

    public void AddCustomer(Customer customer)
    {
        _dal.Customers.Add(customer);
    }

    public void AddAccount(Account account, string cpr)
    {
        var customer = _dal.Customers.Find(c => c.CPR == cpr);
        if (customer != null)
        {
            customer.Accounts.Add(account);
        }
    }

    public List<Customer> GetAllCustomers()
    {
        return _dal.Customers;
    }
}
