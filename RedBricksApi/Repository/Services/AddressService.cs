using System.Data;
using System.Net;
using Microsoft.Data.SqlClient;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;

namespace RedBricksApi.Repository.Services
{
    public class AddressService(IConfiguration configuration) : IAddressService
    {
        private string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        public async Task AddAddress(Address address)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewAddress", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Line1", address.Line1);
                command.Parameters.AddWithValue("@Line2", address.Line2);
                command.Parameters.AddWithValue("@Line3", address.Line3);
                command.Parameters.AddWithValue("@CityCode", address.City);
                command.Parameters.AddWithValue("@CountryCode", address.Country);
                command.Parameters.AddWithValue("@Location", address.Location);
                command.Parameters.AddWithValue("@UserId", address.UserId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteAddress(int addressId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteAddressById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@AddressId", addressId);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<Address> GetAddressById(int addressId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetAddressById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@AddressId", addressId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                Address address = new Address();
                while (await reader.ReadAsync())
                {
                    address.AddressId= reader.GetInt32(0);
                    address.Line1 = reader.GetString(1);
                    address.Line2 = reader.GetString(2);
                    address.Line3 = reader.GetString(3);
                    address.City = reader.GetInt32(4);
                    address.Country=reader.GetInt32(5);
                    address.UserId= reader.GetInt32(6);
                    address.Location = reader.GetString(7);
                    address.CreatedOn = reader.GetDateTime(8);
                    address.UpdatedOn= reader.GetDateTime(9);
                }
                return address;
            }
            catch (Exception ex)
            {
                //throw new NotImplementedException();
                throw;            }
        }
        public async Task<IEnumerable<Address>> GetAddress(int userId)
        {
            try
            {
                var addresses = new List<Address>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetAddressList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserId", userId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    addresses.Add(new Address
                    {
                        AddressId = reader.GetInt32(0),
                        Line1 = reader.GetString(1),
                        Line2 = reader.GetString(2),
                        Line3 = reader.GetString(3),
                        City = reader.GetInt32(4),
                        Country = reader.GetInt32(5),
                        UserId = reader.GetInt32(6),
                        Location = reader.GetString(7),
                        CreatedOn=reader.GetDateTime(8),
                        UpdatedOn=reader.GetDateTime(9)
                        
                    });
                }
                return addresses;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public async Task UpdateAddress(Address address)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateAddress", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", address.AddressId);
                command.Parameters.AddWithValue("@Line1", address.Line1);
                command.Parameters.AddWithValue("@Line2", address.Line2);
                command.Parameters.AddWithValue("@Line3", address.Line3);
                command.Parameters.AddWithValue("@CityCode", address.City);
                command.Parameters.AddWithValue("@CountryCode", address.Country);
                command.Parameters.AddWithValue("@Location", address.Location);
                command.Parameters.AddWithValue("@UserId", address.UserId);
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
