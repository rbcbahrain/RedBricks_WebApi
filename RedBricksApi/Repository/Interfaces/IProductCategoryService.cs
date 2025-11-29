using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface IProductCategoryService
    {
        public Task AddProductCategory(ProductCategory productcategory);
        public Task UpdateProductCategory(ProductCategory productcategory);
        public Task DeleteProductCategory(int id);
        public Task<IEnumerable<ProductCategory>> GetProductCategories();
        public Task<ProductCategory> GetProductCategoryById(int id);
    }
}
