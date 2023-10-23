using Microsoft.EntityFrameworkCore;
using PlukListe_webApp.Models;
using System.Data;

namespace YourProject.Models
{
    public class PluklisteContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
