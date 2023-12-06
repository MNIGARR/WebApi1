using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using WebApi1.Dtos;
using WebApi1.Entities;
using WebApi1.Services.Abstract;

namespace WebApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            var items = _productService.GetAll();
            var dataToReturn = items.Select(p =>
            {
                return new ProductDto
                {
                    ProductName = p.ProductName,
                    Price = (int)p.Price,
                    Discount = (int)p.Discount
                };
            });
            return dataToReturn;
        }

        [HttpGet("GetProduct")]
        public ProductDto? GetProduct(int id)
        {
            var prod = _productService.Get(id);
            if (prod != null)
            {
                var data = new ProductDto
                {
                    ProductName = prod.ProductName,
                    Price = (int)prod.Price,
                    Discount = (int)prod.Discount
                };
                return data;
            }
            return null;
        }

        [HttpPost("PostProduct")]
        public IActionResult Post([FromBody] ProductDto dto)
        {
            try
            {
                var product = new Product
                {
                    ProductName = dto.ProductName,
                    Price = dto.Price,
                    Discount = dto.Discount,
                };
                _productService.Add(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutProduct")]
        public IActionResult Put(int id, [FromBody] ProductDto dto)
        {
            try
            {
                var item = _productService.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                item.Id = id;
                item.ProductName = dto.ProductName;
                item.Price = dto.Price;
                item.Discount = dto.Discount;
                _productService.Update(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult Delete(int id)
        {
            try
            {
                var item = _productService.Get(id);
                if (item == null)
                {
                    return NotFound();
                }
                _productService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
