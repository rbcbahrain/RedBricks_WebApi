using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface ICartService
    {

        public Task AddCart(Cart cart);
        public Task UpdateCart(Cart cart);
        public Task DeleteCartById(int id);
        public Task<IEnumerable<Cart>> GetCarts();
        public Task<Cart> GetCartById(int id);
    }
}
