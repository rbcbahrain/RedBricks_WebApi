using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface IOrderItemService
    {
        public Task AddOrderItems(OrderItems orderitems);
        public Task UpdateOrderItems(OrderItems orderitems);
        public Task DeleteOrderItems(int id);
        public Task<IEnumerable<OrderItems>> GetOrderItemsList();
        public Task<OrderItems> GetOrderItemsById(int id);
    }
}
