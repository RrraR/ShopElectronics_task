using ShopElectronics.Data.Entities;

namespace ShopElectronics.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<ICollection<User>> GetAllUsers();
    public Task<User> GetUser(string username, string? password);
    public Task<User> GetUserByCartId(int cartId);
    public Task<bool> CheckIfExistingUser(string username);
    public Task<User> RegisterUser(string username, string password);

}