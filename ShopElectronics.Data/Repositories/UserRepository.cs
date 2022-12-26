using Microsoft.EntityFrameworkCore;
using ShopElectronics.Data.Entities;
using ShopElectronics.Data.Repositories.Interfaces;

namespace ShopElectronics.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ShopElectronicsDbContext _shopElectronicsDbContext;

    public UserRepository(ShopElectronicsDbContext shopElectronicsDbContext)
    {
        _shopElectronicsDbContext = shopElectronicsDbContext;
    }

    public async Task<ICollection<User>> GetAllUsers()
    {
        var users = await _shopElectronicsDbContext.Users.ToListAsync();
        return users;
    }

    public async Task<User> GetUser(string username, string password)
    {
        var user = await _shopElectronicsDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        return user;
    }
}