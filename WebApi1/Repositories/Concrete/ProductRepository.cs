using WebApi1.Data;
using WebApi1.Entities;
using WebApi1.Repositories.Abstract;

namespace WebApi1.Repositories.Concrete
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public void Add(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _context.Products.Remove(entity);
            _context.SaveChanges();
        }

        public Product Get(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            var products = _context.Products;
            return products;
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
            _context.SaveChanges();
        }
    }
}
