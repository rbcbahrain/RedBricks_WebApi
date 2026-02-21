using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface IProductService
    {

        public Task<IEnumerable<Product>> GetProductListAsync();

        public Task<Product> GetProductByIdAsync(int Id);

        public Task AddProductAsync(Product product);

        public Task UpdateProductAsync(Product product);

        public Task DeleteProductAsync(int Id);
        public Task<bool> CheckProductExistAsync(int id,string productName);
    }
}
