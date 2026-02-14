using Microsoft.Data.SqlClient;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;

namespace RedBricksApi.Repository.Services
{
    public class ProductCategoryService(IConfiguration configuration) : IProductCategoryService
    {
        private string connectionString = configuration.GetConnectionString("DefaultConnection")!;
      

        public async Task DeleteProductCategory(int id)
        {
            try
            {
                using var connection=new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteCategoryById", connection)
                {
                    CommandType= System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductCategory> GetProductCategoryById(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetCategoryById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();   
                using var reader = await command.ExecuteReaderAsync();
                ProductCategory productCategory = new ProductCategory();
                while (await reader.ReadAsync()) 
                {
                    productCategory.Id = reader.GetInt32(0);
                    productCategory.Name = reader.GetString(1);
                    productCategory.Description = reader.GetString(2);
                    productCategory.FileName=reader.GetString(3);
                    productCategory.Status = reader.GetBoolean(4);
                    productCategory.CreatedOn = reader.GetDateTime(5);
                    productCategory.UpdatedOn = reader.GetDateTime(6);
                }
                return productCategory;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
               
        }

        public  async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            try
            {
                var  productCategories = new List<ProductCategory>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetCategoryList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    productCategories.Add(new ProductCategory
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        FileName = reader.GetString(3),
                        Status = reader.GetBoolean(4),
                        CreatedOn = reader.GetDateTime(5),
                        UpdatedOn = reader.GetDateTime(6)
                    });
                }
                return productCategories;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }
       
        public async Task AddProductCategory(ProductCategory productCategory)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewCategory", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", productCategory.Name);
                command.Parameters.AddWithValue("@Description", productCategory.Description);
                command.Parameters.AddWithValue("@FilName", productCategory.FileName);
                command.Parameters.AddWithValue("@Status", productCategory.Status);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            
        }

        public async Task UpdateProductCategory(ProductCategory productCategory)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateCategory", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", productCategory.Id);
                command.Parameters.AddWithValue("@Name", productCategory.Name);
                command.Parameters.AddWithValue("@Description", productCategory.Description);
                command.Parameters.AddWithValue("@FilName", productCategory.FileName);
                command.Parameters.AddWithValue("@Status", productCategory.Status);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<bool> CheckCategoryExistAsync(int id, string categoryName)
        {
            try
            {

                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("CheckCategoryExist", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", categoryName);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
