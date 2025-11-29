using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface IOrderService
    {
        public Task AddOrders(Orders orders);
        public Task UpdateOrders(Orders orders);
        public Task DeleteOrdersById(int id);
        public Task<IEnumerable<Orders>> GetOrderList();
        public Task<Orders> GetOrdersById(int id);
    }
}
