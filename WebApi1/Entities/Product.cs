using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi1.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
