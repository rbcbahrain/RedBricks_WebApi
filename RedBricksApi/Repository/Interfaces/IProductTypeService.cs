using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface IProductTypeService
    {
        public Task AddProductType(ProductType productType);
        public Task UpdateProductType(ProductType productType);
        public Task DeleteProductType(int id);
        public Task<IEnumerable<ProductType>> GetProductTypes();
        public Task<ProductType> GetProductTypeById(int id);
        public Task<bool> CheckProductTypeExistAsync(string productTypeName);
    }
}
