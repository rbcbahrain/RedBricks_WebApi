using RedBricksApi.Models;

namespace RedBricksApi.Repository.Interfaces
{
    public interface IAddressService
    {
        public Task AddAddress(Address address);
        public Task UpdateAddress(Address address);
        public Task DeleteAddress(int addressId);
        public Task<IEnumerable<Address>> GetAddress();
        public Task<Address> GetAddressById(int addressId);
    }
}
