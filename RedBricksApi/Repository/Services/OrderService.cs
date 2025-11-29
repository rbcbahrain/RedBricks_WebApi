using Microsoft.Data.SqlClient;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;

namespace RedBricksApi.Repository.Services
{
    public class OrderService(IConfiguration configuration) : IOrderService
    {
        private string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        public async Task AddOrders(Orders orders)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewOrders", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", orders.Id);
                command.Parameters.AddWithValue("@UserId", orders.UserId);
                command.Parameters.AddWithValue("@TotalAmount", orders.TotalAmount);
                command.Parameters.AddWithValue("@ShippingAddress", orders.ShippingAddress);
                command.Parameters.AddWithValue("@Status", orders.Status);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteOrdersById(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteOrdersById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<Orders>> GetOrderList()
        {
            try
            {
                var orderlist = new List<Orders>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetOrdersList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    orderlist.Add(new Orders
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        TotalAmount=reader.GetDecimal(2),
                        ShippingAddress=reader.GetString(3),
                        Status= reader.GetInt16(4),
                        CreatedOn = reader.GetDateTime(5),
                        UpdatedOn = reader.GetDateTime(6)
                    });
                }
                return orderlist;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }

        public async Task<Orders> GetOrdersById(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetOrdersById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                Orders orders = new Orders();
                while (await reader.ReadAsync())
                {
                    orders.Id = reader.GetInt32(0);
                    orders.UserId = reader.GetInt32(1);
                    orders.TotalAmount = reader.GetDecimal(2);
                    orders.ShippingAddress = reader.GetString(3);
                    orders.Status = reader.GetInt16(4);
                    orders.CreatedOn = reader.GetDateTime(5);
                    orders.UpdatedOn = reader.GetDateTime(6);
                }
                return orders;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task UpdateOrders(Orders orders)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateOrders", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", orders.Id);
                command.Parameters.AddWithValue("@UserId", orders.UserId);
                command.Parameters.AddWithValue("@TotalAmount", orders.TotalAmount);
                command.Parameters.AddWithValue("@ShippingAddress", orders.ShippingAddress);
                command.Parameters.AddWithValue("@Status", orders.Status);

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
