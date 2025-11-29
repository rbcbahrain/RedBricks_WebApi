using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;

namespace RedBricksApi.Repository.Services
{
    public class OrderItemService(IConfiguration configuration) : IOrderItemService
    {
        private string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        public async Task AddOrderItems(OrderItems orderitems)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewOrderItems", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", orderitems.Id);
                command.Parameters.AddWithValue("@OrderId", orderitems.OrderId);
                command.Parameters.AddWithValue("@ServiceId", orderitems.ServiceId);
                command.Parameters.AddWithValue("@Quantity", orderitems.Quantity);
                command.Parameters.AddWithValue("@Price", orderitems.Price);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteOrderItems(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteOrderItemsById", connection)
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

        public async Task<OrderItems> GetOrderItemsById(int id)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetOrderItemsById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                OrderItems orderitems = new OrderItems();
                while (await reader.ReadAsync())
                {
                    orderitems.Id = reader.GetInt32(0);
                    orderitems.OrderId = reader.GetInt32(1);
                    orderitems.ServiceId = reader.GetInt32(2);
                    orderitems.Quantity = reader.GetDecimal(3);
                    orderitems.Price = reader.GetDecimal(4);
                }
                return orderitems;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        public async Task<IEnumerable<OrderItems>> GetOrderItemsList()
        {
            try
            {
                var orderitemslist = new List<OrderItems>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetOrderItemsList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    orderitemslist.Add(new OrderItems
                    {
                        Id = reader.GetInt32(0),
                        OrderId = reader.GetInt32(1),
                        ServiceId = reader.GetInt32(2),
                        Quantity = reader.GetDecimal(3),
                        Price = reader.GetDecimal(4),
                    });
                }
                return orderitemslist;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task UpdateOrderItems(OrderItems orderitems)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateOrderItems", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", orderitems.Id);
                command.Parameters.AddWithValue("@OrderId", orderitems.OrderId);
                command.Parameters.AddWithValue("@ServiceId", orderitems.ServiceId);
                command.Parameters.AddWithValue("@Quantity", orderitems.Quantity);
                command.Parameters.AddWithValue("@Price", orderitems.Price);

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
