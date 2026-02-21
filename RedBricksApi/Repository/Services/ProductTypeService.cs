using Microsoft.Data.SqlClient;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;

namespace RedBricksApi.Repository.Services
{
    public class ProductTypeService(IConfiguration configuration) : IProductTypeService
    {
        private string connectionString = configuration.GetConnectionString("DefaultConnection")!;
      

        public async Task DeleteProductType(int id)
        {
            try
            {
                using var connection=new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteTypeById", connection)
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

        public async Task<ProductType> GetProductTypeById(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetTypeById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();   
                using var reader = await command.ExecuteReaderAsync();
                ProductType productType = new ProductType();
                while (await reader.ReadAsync()) 
                {
                    productType.TypeId = reader.GetInt32(0);
                    productType.Name = reader.GetString(1);
                    productType.Description = reader.GetString(2);
                    productType.FileName=reader.GetString(3);
                    productType.CategoryId = reader.GetInt32(4);
                    productType.Status = reader.GetBoolean(5);
                    productType.CreatedOn = reader.GetDateTime(6);
                    productType.UpdatedOn = reader.GetDateTime(7);
                }
                return productType;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
               
        }

        public  async Task<IEnumerable<ProductType>> GetProductTypes()
        {
            try
            {
                var  productTypes = new List<ProductType>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetTypeList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    productTypes.Add(new ProductType
                    {
                        TypeId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        FileName = reader.GetString(3),
                        CategoryId = reader.GetInt32(4),
                        Status = reader.GetBoolean(5),
                        CreatedOn = reader.GetDateTime(6),
                        UpdatedOn = reader.GetDateTime(7)
                    });
                }
                return productTypes;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }
       
        public async Task AddProductType(ProductType productType)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewType", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", productType.Name);
                command.Parameters.AddWithValue("@Description", productType.Description);
                command.Parameters.AddWithValue("@FileName", productType.FileName);
                command.Parameters.AddWithValue("@CategoryId", productType.CategoryId);
                command.Parameters.AddWithValue("@Status", productType.Status);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            
        }

        public async Task UpdateProductType(ProductType productType)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateType", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", productType.TypeId);
                command.Parameters.AddWithValue("@Name", productType.Name);
                command.Parameters.AddWithValue("@Description", productType.Description);
                command.Parameters.AddWithValue("@FilName", productType.FileName);
                command.Parameters.AddWithValue("@CategoryId", productType.CategoryId);
                command.Parameters.AddWithValue("@Status", productType.Status);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<bool> CheckProductTypeExistAsync(int id, string produtTypeName)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("CheckTypeExist", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", produtTypeName);
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
