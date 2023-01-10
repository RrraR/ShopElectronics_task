using ShopElectronics.Data.Entities;
using ShopElectronics.Services.Models.Dto;

namespace ShopElectronics.Services.Services.Interfaces;

public interface IUserService
{
    public Task<ICollection<UserDto>> GetAllUsers();
    public Task<bool> GetUser(string username, string password);
    public Task<bool> IsAnExistingUser(string username);
    public Task<string> GetUserRole(string userName);
    public Task<UserDto> RegisterUser(string username, string password);
}