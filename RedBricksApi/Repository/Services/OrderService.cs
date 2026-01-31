using Microsoft.Data.SqlClient;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;

namespace RedBricksApi.Repository.Services
{
    public class OrderService(IConfiguration configuration) : IOrderService
    {
        private string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        public async Task AddOrders(int userId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("OrderCheckout", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserId", userId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task DeleteOrdersById(int orderId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteOrdersById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", orderId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<IEnumerable<Orders>> GetOrderList(int userId)
        {
            try
            {
                var orderlist = new List<Orders>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetOrdersList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserId", userId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    orderlist.Add(new Orders
                    {
                        OrderId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        TotalAmount = reader.GetDecimal(2),
                        Status = reader.GetInt16(3),
                        CreatedOn = reader.GetDateTime(4),
                        UpdatedOn = reader.GetDateTime(5)
                    });
                }
                return orderlist;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }
        public async Task<IEnumerable<Orders>> GetAllOrderList()
        {
            try
            {
                var orderlist = new List<Orders>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetAllOrdersList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    orderlist.Add(new Orders
                    {
                        OrderId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        TotalAmount=reader.GetDecimal(2),
                        Status= reader.GetInt16(3),
                        CreatedOn = reader.GetDateTime(4),
                        UpdatedOn = reader.GetDateTime(5)
                    });
                }
                return orderlist;
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }
        }
        public async Task<Orders> GetOrdersById(int orderId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetOrdersById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", orderId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                Orders orders = new Orders();
                while (await reader.ReadAsync())
                {
                    orders.OrderId = reader.GetInt32(0);
                    orders.UserId = reader.GetInt32(1);
                    orders.TotalAmount = reader.GetDecimal(2);
                    orders.Status = reader.GetInt16(3);
                    orders.CreatedOn = reader.GetDateTime(4);
                    orders.UpdatedOn = reader.GetDateTime(5);
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
                command.Parameters.AddWithValue("@Id", orders.OrderId);
                command.Parameters.AddWithValue("@UserId", orders.UserId);
                command.Parameters.AddWithValue("@TotalAmount", orders.TotalAmount);
                command.Parameters.AddWithValue("@Status", orders.Status);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task AddOrderItems(OrderItems orderitems)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewOrderItems", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", orderitems.OrderItemId);
                command.Parameters.AddWithValue("@OrderId", orderitems.OrderId);
                command.Parameters.AddWithValue("@ServiceId", orderitems.ServiceId);
                command.Parameters.AddWithValue("@Quantity", orderitems.Quantity);
                command.Parameters.AddWithValue("@Price", orderitems.Price);
                command.Parameters.AddWithValue("@AddressId", orderitems.AddressId);
                command.Parameters.AddWithValue("@Servicedate", orderitems.ServiceDate);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteOrderItem(int orderItemId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteOrderItemsById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", orderItemId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<OrderItems> GetOrderItemsById(int orderItemId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetOrderItemsById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", orderItemId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                OrderItems orderitems = new OrderItems();
                while (await reader.ReadAsync())
                {
                    orderitems.OrderItemId = reader.GetInt32(0);
                    orderitems.OrderId = reader.GetInt32(1);
                    orderitems.ServiceId = reader.GetInt32(2);
                    orderitems.Quantity = reader.GetDecimal(3);
                    orderitems.Price = reader.GetDecimal(4);
                    orderitems.ServiceId= reader.GetInt32(5);
                    orderitems.ServiceDate = reader.GetDateTime(6);
                }
                return orderitems;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }

        }

        public async Task<IEnumerable<OrderItems>> GetOrderItemsList(int orderId)
        {
            try
            {
                var orderitemslist = new List<OrderItems>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetOrderItemsList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("OrderId", orderId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    orderitemslist.Add(new OrderItems
                    {
                        OrderItemId = reader.GetInt32(0),
                        OrderId = reader.GetInt32(1),
                        ServiceId = reader.GetInt32(2),
                        Quantity = reader.GetDecimal(3),
                        Price = reader.GetDecimal(4),
                        AddressId=reader.GetInt32(5),
                        ServiceDate = reader.GetDateTime(6),
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
                command.Parameters.AddWithValue("@Id", orderitems.OrderItemId);
                command.Parameters.AddWithValue("@OrderId", orderitems.OrderId);
                command.Parameters.AddWithValue("@ServiceId", orderitems.ServiceId);
                command.Parameters.AddWithValue("@Quantity", orderitems.Quantity);
                command.Parameters.AddWithValue("@Price", orderitems.Price);
                command.Parameters.AddWithValue("@AddressId", orderitems.AddressId);  
                command.Parameters.AddWithValue("@ServiceDate", orderitems.ServiceDate);
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
