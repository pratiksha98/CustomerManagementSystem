using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(string fullName, DateOnly dateOfBirth);
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age);
        Task<Customer> UpdateCustomerAsync(Guid customerId, string fullName, DateOnly dateOfBirth);

        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
