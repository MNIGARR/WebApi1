using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi1.Dtos;
using WebApi1.Entities;
using WebApi1.Services.Abstract;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<OrderDto> GetOrders()
        {
            var items = _orderService.GetAll();
            var dataToReturn = items.Select(o =>
            {
                return new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    ProductId = o.ProductId,
                    Product = o.Product,
                    CustomerId = o.CustomerId,
                    Customer = o.Customer,
                };
            });
            return dataToReturn;
        }

        [HttpGet("GetOrder")]
        public OrderDto? GetOrder(int id)
        {
            var order = _orderService.Get(id);
            if (order != null)
            {
                var data = new OrderDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    ProductId = order.ProductId,
                    Product = order.Product,
                    CustomerId = order.CustomerId,
                    Customer = order.Customer,
                };
                return data;
            }
            return null;
        }

        [HttpPost("PostOrder")]
        public IActionResult Post([FromBody] OrderDto dto)
        {
            try
            {
                var order = new Order
                {
                    //Id = dto.Id,
                    OrderDate = dto.OrderDate,
                    ProductId = dto.ProductId,
                    Product = dto.Product,
                    CustomerId = dto.CustomerId,
                    Customer = dto.Customer,
                };
                _orderService.Add(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("PutOrder")]
        public IActionResult Put(int id, [FromBody] OrderDto dto)
        {
            try
            {
                var item = _orderService.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                item.Id = id;
                item.OrderDate = dto.OrderDate;
                item.ProductId = dto.ProductId;
                item.Product = dto.Product;
                item.CustomerId = dto.CustomerId;
                item.Customer = dto.Customer;
                _orderService.Update(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteOrder")]
        public IActionResult Delete(int id)
        {
            try
            {
                var item = _orderService.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                _orderService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
