using WebApi1.Entities;
using WebApi1.Repositories.Abstract;
using WebApi1.Repositories.Concrete;
using WebApi1.Services.Abstract;

namespace WebApi1.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public void Add(Order entity)
        {
            _orderRepository.Add(entity);
        }

        public void Delete(int id)
        {
            var item = _orderRepository.Get(id);
            _orderRepository.Delete(item);
        }

        public Order Get(int id)
        {
            return _orderRepository.Get(id);
        }
        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }
        public void Update(Order entity)
        {
            _orderRepository.Update(entity);
        }
    }
}
