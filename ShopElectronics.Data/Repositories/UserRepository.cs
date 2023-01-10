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

    public async Task<User> GetUser(string username, string? password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return await _shopElectronicsDbContext.Users.FirstOrDefaultAsync(u =>
                u.UserName == username);
        }
        
        return await _shopElectronicsDbContext.Users.FirstOrDefaultAsync(u =>
            u.UserName == username && u.Password == password);

    }

    public async Task<User> GetUserByCartId(int cartId)
    {
        return await _shopElectronicsDbContext.Users.FirstOrDefaultAsync(u => u.Cart.Id == cartId);
    }

    public async Task<bool> CheckIfExistingUser(string username)
    {
        return await _shopElectronicsDbContext.Users.AnyAsync(u => u.UserName == username);
    }

    public async Task<User> RegisterUser(string username, string password)
    {
        var newUser = new User()
        {
            UserName = username,
            Password = password
        };

        await _shopElectronicsDbContext.Users.AddAsync(newUser);
        await _shopElectronicsDbContext.SaveChangesAsync();

        var cart = new Cart()
        {
            UserId = _shopElectronicsDbContext.Users.FirstOrDefault(u => u.UserName == username).Id
        };
        await _shopElectronicsDbContext.Carts.AddAsync(cart);
        await _shopElectronicsDbContext.SaveChangesAsync();
        
        var user = await _shopElectronicsDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        return user;
        // var newCart = new Cart()
        // {
        //     UserId = _shopElectronicsDbContext.Users.FirstOrDefault(u => u.UserName == username).Id
        // };
        // await _shopElectronicsDbContext.Carts.AddAsync(newCart);
        // await _shopElectronicsDbContext.SaveChangesAsync();

        // return await _shopElectronicsDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}