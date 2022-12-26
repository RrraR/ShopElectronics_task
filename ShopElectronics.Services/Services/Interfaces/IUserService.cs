using ShopElectronics.Services.Models.Dto;

namespace ShopElectronics.Services.Services.Interfaces;

public interface IUserService
{
    public Task<ICollection<UserDto>> GetAllUsers();
    public Task<bool> GetUser(string username, string password);
}