using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age);
        Task<Customer> UpdateCustomerAsync(Customer customer);

        Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
