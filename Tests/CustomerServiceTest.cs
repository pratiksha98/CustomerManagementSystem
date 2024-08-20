using BusinessLogicLayer.Services;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using FluentAssertions;
using Moq;

namespace Tests
{
    public class CustomerServiceTest
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<HttpClient> _httpClientMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTest()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _httpClientMock = new Mock<HttpClient>();
            _customerService = new CustomerService(_customerRepositoryMock.Object, _httpClientMock.Object);
        }
        [Fact]
        public async Task CustomerService_CreateCustomerAsync_ShouldReturnCustomer()
        {
            // Arrange
            var fullName = "Pratiksha";
            var dateOfBirth = new DateOnly(1972, 1, 1);
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FullName = fullName,
                DateOfBirth = dateOfBirth,
                ProfileImage = "<svg></svg>"
            };

            
            _customerRepositoryMock.Setup(repo => repo.AddCustomerAsync(It.IsAny<Customer>()))
                .ReturnsAsync(customer);

            // Act
            var result = await _customerService.CreateCustomerAsync(fullName, dateOfBirth);

            // Assert
            result.Should().NotBeNull();
            result.FullName.Should().Be(fullName);
            result.DateOfBirth.Should().Be(dateOfBirth);
            result.ProfileImage.Should().Be("<svg></svg>");
        }

        [Fact]
        public async Task CustomerService_GetCustomerByIdAsync_ShouldReturnCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var expectedCustomer = new Customer { CustomerId = customerId, FullName = "Pratiksha Chaudhary", DateOfBirth = new DateOnly(1990, 08, 16) };

            _customerRepositoryMock.Setup(repo => repo.GetCustomerByIdAsync(customerId))
                .ReturnsAsync(expectedCustomer);

            // Act
            var customer = await _customerService.GetCustomerByIdAsync(customerId);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(expectedCustomer, customer);
        }

        [Fact]
        public async Task CustomerService_GetCustomersByAgeAsync_ShouldReturnCustomers()
        {
            // Arrange
            var age = 20;
            var expectedCustomers = new List<Customer>
        {
            new Customer { CustomerId = Guid.NewGuid(), FullName = "Pratiksha Chaudhary", DateOfBirth = new DateOnly(2001, 01, 17) },
            new Customer { CustomerId = Guid.NewGuid(), FullName = "Eesha Tripathi", DateOfBirth = new DateOnly(1999, 08, 16) }
        };

            _customerRepositoryMock.Setup(repo => repo.GetCustomersByAgeAsync(age))
                .ReturnsAsync(expectedCustomers);

            // Act
            var customers = await _customerService.GetCustomersByAgeAsync(age);

            // Assert
            Assert.NotNull(customers);
            Assert.Equal(expectedCustomers, customers);
        }


        [Fact]
        public async Task CustomerService_UpdateCustomerAsync_ShouldUpdateCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var fullName = "Pratiksha";
            var dateOfBirth = new DateOnly(2000, 08, 16);
            var existingCustomer = new Customer { CustomerId = customerId, FullName = "Pratiksha", DateOfBirth = new DateOnly(2000, 08, 16) };

            _customerRepositoryMock.Setup(repo => repo.GetCustomerByIdAsync(customerId))
                .ReturnsAsync(existingCustomer);

            _customerRepositoryMock.Setup(repo => repo.UpdateCustomerAsync(It.IsAny<Customer>()))
                .ReturnsAsync((Customer customer) => customer);

            // Act
            var updatedCustomer = await _customerService.UpdateCustomerAsync(customerId, fullName, dateOfBirth);

            // Assert
            Assert.NotNull(updatedCustomer);
            Assert.Equal(fullName, updatedCustomer.FullName);
            Assert.Equal(dateOfBirth, updatedCustomer.DateOfBirth);
            _customerRepositoryMock.Verify(repo => repo.UpdateCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task CustomerService_GetAllCustomersAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var expectedCustomers = new List<Customer>
        {
            new Customer { CustomerId = Guid.NewGuid(), FullName = "Pratiksha", DateOfBirth = new DateOnly(2000, 08, 08) },
            new Customer { CustomerId = Guid.NewGuid(), FullName = "Divya", DateOfBirth = new DateOnly(1999, 1, 1) }
        };

            _customerRepositoryMock.Setup(repo => repo.GetAllCustomers())
                .ReturnsAsync(expectedCustomers);

            // Act
            var customers = await _customerService.GetAllCustomersAsync();

            // Assert
            Assert.NotNull(customers);
            Assert.Equal(expectedCustomers, customers);
        }



    }




}