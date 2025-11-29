using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface ICartItemService
    {
        public Task AddCartItems(CartItems cartitems);
        public Task UpdateCartItems(CartItems cartitems);
        public Task DeleteCartItems(int id);
        public Task<IEnumerable<CartItems>> GetCartItems();
        public Task<CartItems> GetCartItemsById(int id);
    }
}
