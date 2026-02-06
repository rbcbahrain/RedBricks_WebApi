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

        public async Task DeleteCartById(int cartId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteCartById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", cartId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cart> GetCartById(int cartId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetCartById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", cartId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                Cart cart = new Cart();
                while (await reader.ReadAsync())
                {
                    cart.CartId = reader.GetInt32(0);
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
                        CartId = reader.GetInt32(0),
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
                command.Parameters.AddWithValue("@Id", cart.CartId);
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
                command.Parameters.AddWithValue("@AddressId", cartitems.AddressId);
                command.Parameters.AddWithValue("@ServiceDate", cartitems.ServiceDate); 

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task DeleteCartItems(int cartItemid)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteCartItemsById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", cartItemid);
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
                        CartItemId = reader.GetInt32(reader.GetOrdinal("ID")),
                        CartId = reader.GetInt32(reader.GetOrdinal("CARTID")),
                        ServiceId = reader.GetInt32(reader.GetOrdinal("SERVICEID")),
                        ServiceName = reader.IsDBNull(reader.GetOrdinal("SERVICENAME")) ? null : reader.GetString(reader.GetOrdinal("SERVICENAME")),
                        Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY")),
                        AddressId = reader.IsDBNull(reader.GetOrdinal("ADDRESSID")) ? 0 : reader.GetInt32(reader.GetOrdinal("ADDRESSID")),
                        AddressName = reader.IsDBNull(reader.GetOrdinal("SERVICEADDRESS")) ? null : reader.GetString(reader.GetOrdinal("SERVICEADDRESS")),
                        ServiceDate = reader.GetFieldValue<DateTime>(reader.GetOrdinal("SERVICEDATE")),
                        Price = reader.IsDBNull(reader.GetOrdinal("PRICE")) ? 0 : reader.GetDecimal(reader.GetOrdinal("PRICE")),
                        Location = reader.IsDBNull(reader.GetOrdinal("LOCATION")) ? null : reader.GetString(reader.GetOrdinal("LOCATION")),
                        AddedAt = reader.GetFieldValue<DateTime>(reader.GetOrdinal("ADDEDAT")),
                        
                    });
                }
                return cartitemlist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CartItems> GetCartItemsById(int cartItemId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetCartItemsById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", cartItemId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                CartItems cartitems = new CartItems();
                while (await reader.ReadAsync())
                {
                    cartitems.CartItemId = reader.GetInt32(0);
                    cartitems.CartId = reader.GetInt32(1);
                    cartitems.ServiceId = reader.GetInt32(2);
                    cartitems.ServiceName = reader.GetString(3);
                    cartitems.Quantity = reader.GetInt32(4);
                    cartitems.AddressId = reader.GetInt32(5);
                    cartitems.AddressName = reader.GetString(6);
                    cartitems.ServiceDate = reader.GetDateTime(7);
                    cartitems.Price = reader.GetDecimal(8);
                    cartitems.AddedAt = reader.GetDateTime(9);
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
                command.Parameters.AddWithValue("@Id", cartitems.CartItemId);
                command.Parameters.AddWithValue("@CartId", cartitems.CartId);
                command.Parameters.AddWithValue("@ServiceId", cartitems.ServiceId);
                command.Parameters.AddWithValue("@Quantity", cartitems.Quantity);
                command.Parameters.AddWithValue("@Price", cartitems.Price);
                command.Parameters.AddWithValue("@AddressId", cartitems.AddressId);
                command.Parameters.AddWithValue("@ServiceDate", cartitems.ServiceDate);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task UpdateCartItemsQty(CartItems cartitems)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateCartItemQty", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CartItemId",cartitems.CartItemId);
                command.Parameters.AddWithValue("@Quantity", cartitems.Quantity);
 
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
