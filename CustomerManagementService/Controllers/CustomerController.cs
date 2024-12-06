using CustomerManagementService.BusinessLayer;
using CustomerManagementService.Models.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse>> GetCustomers([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var customers = await _customerService.GetAll(pageNumber, pageSize);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewCustomerModel>> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetById(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<ViewCustomerModel>> PostCustomer(CreateCustomerModel customer)
        {
            var result = await _customerService.Create(customer);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, UpdateCustomerModel customer)
        {
            if (id != customer.Id)
                return BadRequest();

            await _customerService.Update(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _customerService.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
