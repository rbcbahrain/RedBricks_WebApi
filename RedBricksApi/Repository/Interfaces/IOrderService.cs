using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface IOrderService
    {
        public Task AddOrders(int userId);
        public Task UpdateOrders(Orders orders);
        public Task DeleteOrdersById(int orderId);
        public Task<IEnumerable<Orders>> GetOrderList(int userId);
        public Task<IEnumerable<Orders>> GetAllOrderList();
        public Task<Orders> GetOrdersById(int orderId);
        public Task AddOrderItems(OrderItems orderItems);
        public Task UpdateOrderItems(OrderItems orderItems);
        public Task DeleteOrderItem(int oderItemId);
        public Task<IEnumerable<OrderItems>> GetOrderItemsList(int orderId);
        public Task<OrderItems> GetOrderItemsById(int oderItemId);
    }
}
