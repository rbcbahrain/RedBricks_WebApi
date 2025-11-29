using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Localization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;


namespace RedBricksApi.Repository.Services
{
    public class ProductService(IConfiguration configuration) : IProductService
    {
        private string connectionString = configuration.GetConnectionString("DefaultConnection")!;
       
        public async Task<IEnumerable<Product>> GetProductListAsync()
        {
            try
            {
                var products = new List<Product>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetProductList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    products.Add(new Product
                    {
                        ProductId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Type = reader.GetInt32(3),
                        Price = reader.GetDecimal(4),
                        Rating = reader.GetDecimal(5),
                        FileName = reader.GetString(6),
                        Status = reader.GetBoolean(7),
                        CreatedOn = reader.GetDateTime(8),
                        UpdatedOn = reader.GetDateTime(9)
                    });
                }
                return products;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<Product> GetProductByIdAsync(int ProductId)
        {

            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetProductByID", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", ProductId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                Product prodcut= new Product();
                while (await reader.ReadAsync())
                {
                    prodcut.ProductId = reader.GetInt32(0);
                    prodcut.Name = reader.GetString(1);
                    prodcut.Description = reader.GetString(2);
                    prodcut.Type = reader.GetInt32(3);
                    prodcut.Price = reader.GetDecimal(4);
                    prodcut.Rating = reader.GetDecimal(5);
                    prodcut.FileName = reader.GetString(6);
                    prodcut.Status = reader.GetBoolean(7);
                    prodcut.CreatedOn = reader.GetDateTime(8);
                    prodcut.UpdatedOn = reader.GetDateTime(9);
                }
                return prodcut;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task AddProductAsync(Product product)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Type", product.Type);
                command.Parameters.AddWithValue("@Rating", product.Rating);
                command.Parameters.AddWithValue("@FileName", product.FileName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Status", product.Status);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateProduct", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", product.ProductId);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Type", product.Type);
                command.Parameters.AddWithValue("@Rating", product.Rating);
                command.Parameters.AddWithValue("@FileName", product.FileName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Status", product.Status);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task DeleteProductAsync(int ProductId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteProductById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ProductId", ProductId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

    }
}
