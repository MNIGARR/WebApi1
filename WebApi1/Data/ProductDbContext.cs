using Microsoft.EntityFrameworkCore;
using WebApi1.Entities;

namespace WebApi1.Data
{
    public class ProductDbContext: DbContext
    {

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
