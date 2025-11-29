using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUserListAsync();
        public Task<User> GetUserByIdAsync(int userId);
        public Task AddNewUserAsync(User user);
        public Task UpdateUserAsync(User user);
        public Task DeleteUserAsync(int userId);
        public Task<User> CheckUserExistAsync(string userEmail, string userPassword);

    }
}
