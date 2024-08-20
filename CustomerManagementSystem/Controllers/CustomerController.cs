using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace CustomerManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = await _customerService.CreateCustomerAsync(request.FullName, request.DateOfBirth);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);

        }

        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if(customer==null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("{age:int}")]

        public async Task<IActionResult> GetCustomersByAge(int age)
        {
            var customers = await _customerService.GetCustomersByAgeAsync(age);
            return Ok(customers);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        [HttpPatch("id:guid")]

        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var customer = await _customerService.UpdateCustomerAsync(id, request.FullName, request.DateOfBirth);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }

    public class CreateCustomerRequest
    {
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }

    public class UpdateCustomerRequest
    {
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
