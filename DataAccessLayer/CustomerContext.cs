using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options):base(options)
        {
                
        }
       public DbSet<Customer> Customers { get; set; }
    }
}
