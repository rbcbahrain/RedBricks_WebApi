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
                command.Parameters.AddWithValue("@ContactName", address.ContactName);
                command.Parameters.AddWithValue("@ContactNo", address.ContactNo);
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
                    address.ContactName = reader.GetString(1);
                    address.ContactNo= reader.GetString(2);
                    address.Line1 = reader.GetString(3);
                    address.Line2 = reader.GetString(4);
                    address.Line3 = reader.GetString(5);
                    address.City = reader.GetInt32(6);
                    address.Country=reader.GetInt32(7);
                    address.UserId= reader.GetInt32(8);
                    address.Location = reader.GetString(9);
                    address.CreatedOn = reader.GetDateTime(10);
                    address.UpdatedOn= reader.GetDateTime(11);
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
                        AddressId =  reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                        ContactName = reader["CONTACTNAME"] != DBNull.Value ? reader["CONTACTNAME"].ToString() : string.Empty,
                        ContactNo = reader["CONTACTNO"] != DBNull.Value ? reader["CONTACTNO"].ToString() : string.Empty,
                        Line1 = reader["LINE1"] != DBNull.Value ? reader["LINE1"].ToString() : string.Empty, 
                        Line2 = reader["LINE2"] != DBNull.Value ? reader["LINE2"].ToString() : string.Empty, 
                        Line3 = reader["LINE3"] != DBNull.Value ? reader["LINE3"].ToString() : string.Empty, 
                        City = reader["CITY"] != DBNull.Value ? Convert.ToInt32(reader["CITY"]) : 0,
                        Country = reader["COUNTRY"] != DBNull.Value ? Convert.ToInt32(reader["COUNTRY"]) : 0, 
                        UserId = reader["USERID"] != DBNull.Value ? Convert.ToInt32(reader["USERID"]) : 0,  //reader.GetInt32(8),
                        Location =reader["LOCATION"] != DBNull.Value ? reader["LOCATION"].ToString() : string.Empty, // != DBNull.Value ? Convert.ToString(reader["LOCATION"]) : string.Empty, // reader.GetString(9),
                        CreatedOn = reader.GetDateTime(reader.GetOrdinal("CREATEDON")), //reader["CREATEDON"] //reader.GetDateTime(10),
                        UpdatedOn = reader.GetDateTime(reader.GetOrdinal("UpdatedOn"))

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
                command.Parameters.AddWithValue("@ContactName", address.ContactName);
                command.Parameters.AddWithValue("@ContactNo", address.ContactNo);
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
