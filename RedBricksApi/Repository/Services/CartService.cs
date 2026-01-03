using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;

namespace RedBricksApi.Repository.Services
{
    public class CartService (IConfiguration configuration) : ICartService
    {
        private string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        public async Task<int> AddCart(int userId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewCart", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserId", userId);
                SqlParameter outputParams = new SqlParameter("@Id", System.Data.SqlDbType.Int)
                { 
                    Direction = System.Data.ParameterDirection.Output
                };
                command.Parameters.Add(outputParams);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                int result=(int)command.Parameters["@Id"].Value;
                return result;

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteCartById(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteCartById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
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

        public async Task<Cart> GetCartById(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetCartById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                Cart cart = new Cart();
                while (await reader.ReadAsync())
                {
                    cart.Id = reader.GetInt32(0);
                    cart.UserId = reader.GetInt32(1);
                    cart.CreatedOn = reader.GetDateTime(2);
                    cart.UpdatedOn = reader.GetDateTime(3);
                }
                return cart;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<Cart>> GetCarts()
        {
            try
            {
                var carts = new List<Cart>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetCartList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    carts.Add(new Cart
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        CreatedOn = reader.GetDateTime(2),
                        UpdatedOn = reader.GetDateTime(3)
                    });
                }
                return carts;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task UpdateCart(Cart cart)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateCart", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", cart.Id);
                command.Parameters.AddWithValue("@UserId", cart.UserId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task AddCartItems(CartItems cartitems)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewCartItems", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CartId", cartitems.CartId);
                command.Parameters.AddWithValue("@ServiceId", cartitems.ServiceId);
                command.Parameters.AddWithValue("@Quantity", cartitems.Quantity);
                command.Parameters.AddWithValue("@Price", cartitems.Price);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task DeleteCartItems(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteCartItemsById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
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

        public async Task<IEnumerable<CartItems>> GetCartItems(int userId)
        {
            try
            {
                var cartitemlist = new List<CartItems>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetCartItemsList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserId", userId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    cartitemlist.Add(new CartItems
                    {
                        Id = reader.GetInt32(0),
                        CartId = reader.GetInt32(1),
                        ServiceId = reader.GetInt32(2),
                        Quantity = reader.GetDecimal(3),
                        Price = reader.GetDecimal(4),
                        AddedAt = reader.GetDateTime(5)
                    });
                }
                return cartitemlist;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<CartItems> GetCartItemsById(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetCartItemsById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                CartItems cartitems = new CartItems();
                while (await reader.ReadAsync())
                {
                    cartitems.Id = reader.GetInt32(0);
                    cartitems.CartId = reader.GetInt32(1);
                    cartitems.ServiceId = reader.GetInt32(2);
                    cartitems.Quantity = reader.GetDecimal(3);
                    cartitems.Price = reader.GetDecimal(4);
                    cartitems.AddedAt = reader.GetDateTime(5);
                }
                return cartitems;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task UpdateCartItems(CartItems cartitems)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateCartItems", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", cartitems.Id);
                command.Parameters.AddWithValue("@CartId", cartitems.CartId);
                command.Parameters.AddWithValue("@ServiceId", cartitems.ServiceId);
                command.Parameters.AddWithValue("@Quantity", cartitems.Quantity);
                command.Parameters.AddWithValue("@Price", cartitems.Price);

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
