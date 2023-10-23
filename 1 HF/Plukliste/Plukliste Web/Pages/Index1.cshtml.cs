using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic; // Import necessary namespaces
using YourNamespace.Models; // Import your data models namespace

public class CustomersModel : PageModel
{
    public List<Customer> Customers { get; private set; }

    public void OnGet()
    {
        // Fetch your customer data here, for example from a database or a service
        // Populate the Customers property with the fetched data
        Customers = YourDataFetchingMethod();
    }
}
