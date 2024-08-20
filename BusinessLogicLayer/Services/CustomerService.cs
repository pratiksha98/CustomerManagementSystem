using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CustomerService :ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly HttpClient _httpClient;
        public CustomerService(ICustomerRepository customerRepository,HttpClient httpClient)
        {
                _customerRepository = customerRepository;
                 _httpClient = httpClient;
        }
        public async Task<Customer> CreateCustomerAsync(string fullName, DateOnly dateOfBirth)
        {
            var customer = new Customer()
            {
                FullName = fullName,
                DateOfBirth = dateOfBirth

            };
            var profileImageUrl = $"https://ui-avatars.com/api/?name={Uri.EscapeDataString(fullName)}&format=svg";
            var response = await _httpClient.GetAsync(profileImageUrl);

            if (response.IsSuccessStatusCode)
            {
                customer.ProfileImage = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Failed to retrieve profile image.");
            }

            return await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            return await _customerRepository.GetCustomerByIdAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByAgeAsync(int age)
        {
            return await _customerRepository.GetCustomersByAgeAsync(age);
        }

        public async Task<Customer> UpdateCustomerAsync(Guid customerId, string fullName, DateOnly dateOfBirth)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            customer.FullName = fullName;
            customer.DateOfBirth = dateOfBirth;

            return await _customerRepository.UpdateCustomerAsync(customer);
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomers();
        }
    }
}
