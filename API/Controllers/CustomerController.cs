using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerService _customerService { get; set; }

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomersFromFile(IFormFile csvFile)
        {
            var customers = await _customerService.AddCustomersFromFile(csvFile);

            return Ok(customers);
        }

    }
}
