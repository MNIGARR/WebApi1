using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi1.Entities;
using WebApi1.Dtos;
using WebApi1.Services.Abstract;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IEnumerable<CustomerDto> Get()
        {
            var items = _customerService.GetAll();
            var dataToReturn = items.Select(c =>
            {
                return new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                };
            });
            return dataToReturn;
        }

        [HttpGet("GetCustomer")]
        public CustomerDto? GetCustomer(int id)
        {
            var customer = _customerService.Get(id);
            if (customer != null)
            {
                var data = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                };
                return data;
            }
            return null;
        }


        [HttpPost("PostCustomer")]
        public IActionResult Post([FromBody] CustomerDto dto)
        {
            try
            {
                var customer = new Customer
                {
                    Name = dto.Name,
                };
                _customerService.Add(customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutCustomer")]
        public IActionResult Put(int id, [FromBody] CustomerDto dto)
        {
            try
            {
                var item = _customerService.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                item.Name = dto.Name;
                item.Id = dto.Id;
                _customerService.Update(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCustomer")]
        public IActionResult Delete(int id)
        {
            try
            {
                var item = _customerService.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                _customerService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
