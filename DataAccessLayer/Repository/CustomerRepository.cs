using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
                _context = context;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age) {

            var today = DateOnly.FromDateTime(DateTime.Today);
            return await _context.Customers.Where(c => today.Year - c.DateOfBirth.Year == age).ToListAsync();
        }
        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

    }
}
