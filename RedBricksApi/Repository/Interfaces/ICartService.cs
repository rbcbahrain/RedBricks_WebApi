using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface ICartService
    {

        public Task<int> AddCart(int userId);
        public Task UpdateCart(Cart cart);
        public Task DeleteCartById(int cartId);
        public Task<IEnumerable<Cart>> GetCarts();
        public Task<Cart> GetCartById(int cartId);
        public Task AddCartItems(CartItems cartitems);
        public Task UpdateCartItems(CartItems cartitems);
        public Task DeleteCartItems(int cartItemId);
        public Task<IEnumerable<CartItems>> GetCartItems(int userId);
        public Task<CartItems> GetCartItemsById(int cartItemid);
        public Task UpdateCartItemsQty(CartItems cartitems);
    }

}
