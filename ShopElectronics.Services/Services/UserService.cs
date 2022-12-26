using AutoMapper;
using ShopElectronics.Data.Repositories.Interfaces;
using ShopElectronics.Services.Models.Dto;
using ShopElectronics.Services.Services.Interfaces;

namespace ShopElectronics.Services.Services;

public class UserService : IUserService
{
    private readonly IMapper _autoMapper;
    private readonly IUserRepository _userRepository;


    public UserService(IMapper autoMapper, IUserRepository userRepository)
    {
        _autoMapper = autoMapper;
        _userRepository = userRepository;
    }

    public async Task<ICollection<UserDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        return _autoMapper.Map<ICollection<UserDto>>(users);
    }

    public async Task<bool> GetUser(string username, string password)
    {
        var user = await _userRepository.GetUser(username, password);
        // return _autoMapper.Map<UserDto>(user);
        if (user == null)
        {
            return false;
        }

        return true;
    }
}