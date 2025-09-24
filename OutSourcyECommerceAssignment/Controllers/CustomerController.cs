using Microsoft.AspNetCore.Mvc;
using OutSourcyECommerceAssignment.DTOs.CustomerDTOs;
using OutSourcyECommerceAssignment.Models;
using OutSourcyECommerceAssignment.Services.Interfaces;

namespace OutSourcyECommerceAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCusomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound(new { message = "Customer not found." });
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check for unique email
            if (await _customerService.EmailExistsAsync(dto.Email))
                return BadRequest(new { message = "Email already exists." });

            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };

            var createdCustomer = await _customerService.AddAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.Id }, createdCustomer);
        }

    }
}
