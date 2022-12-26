using ShopElectronics.Data.Entities;

namespace ShopElectronics.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ICollection<User>> GetAllUsers();
    public Task<User> GetUser(string username, string password);
}