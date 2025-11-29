
using Microsoft.Data.SqlClient;
using RedBricksApi.Models;
using RedBricksApi.Repository.Interfaces;

namespace RedBricksApi.Repository.Services
{
    public class UserService(IConfiguration configuration) : IUserService
    {
        private string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        public async Task DeleteUserAsync(int userId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("DeleteUserById", connection)
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

        public async Task<IEnumerable<User>>GetUserListAsync()
        {
            try
            {
                var users  = new List<User>();
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetUserList", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(new User
                    {
                        UserId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        Password = reader.GetString(3),
                        Isdcode = reader.GetInt32(4),
                        PhoneNo = reader.GetString(5),
                        NationalId = reader.GetString(6),
                        RoleId = reader.GetInt32(7),
                        Status = reader.GetBoolean(8),
                        CreatedOn = reader.GetDateTime(9),
                        UpdatedOn = reader.GetDateTime(10)
                    });
                }
                return users;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("GetUserById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserId", userId);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                User user = new User();
                while (await reader.ReadAsync())
                {
                    user.UserId = reader.GetInt32(0);
                    user.Name = reader.GetString(1);
                    user.Email = reader.GetString(2);
                    user.Password = reader.GetString(3);
                    user.Isdcode = reader.GetInt32(4);
                    user.PhoneNo = reader.GetString(5);
                    user.NationalId = reader.GetString(6);
                    user.RoleId = reader.GetInt32(7);
                    user.Status = reader.GetBoolean(8);
                    user.CreatedOn = reader.GetDateTime(9);
                    user.UpdatedOn = reader.GetDateTime(10);
                }
                return user;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task AddNewUserAsync(User user)
        {

            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("AddNewUser", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@IsdCode", user.Isdcode);
                command.Parameters.AddWithValue("@PhoneNo", user.PhoneNo);
                command.Parameters.AddWithValue("@NationalId", user.NationalId);
                command.Parameters.AddWithValue("@RoleId", user.RoleId);
                command.Parameters.AddWithValue("@Status", user.Status);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async Task UpdateUserAsync(User user)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("UpdateUser", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@IsdCode", user.Isdcode);
                command.Parameters.AddWithValue("@PhoneNo", user.PhoneNo);
                command.Parameters.AddWithValue("@NationalId", user.NationalId);
                command.Parameters.AddWithValue("@RoleId", user.RoleId);
                command.Parameters.AddWithValue("@Status", user.Status);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
      
        public async Task<User>CheckUserExistAsync(string userEmail, string userPassword)
        {
            try
            {

                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand("CheckUserExist", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Email", userEmail);
                command.Parameters.AddWithValue("@Password", userPassword);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                User user = new User();
                while (await reader.ReadAsync())
                {
                    user.UserId = reader.GetInt32(0);
                    user.Name = reader.GetString(1);
                    user.Email = reader.GetString(2);
                    user.Password = reader.GetString(3);
                    user.Isdcode = reader.GetInt32(4);
                    user.PhoneNo = reader.GetString(5);
                    user.NationalId = reader.GetString(6);
                    user.RoleId = reader.GetInt32(7);
                    user.Status = reader.GetBoolean(8);
                    user.CreatedOn = reader.GetDateTime(9);
                    user.UpdatedOn = reader.GetDateTime(10);
                }
                return user;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
