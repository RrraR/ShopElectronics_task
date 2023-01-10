using AutoMapper;
using ShopElectronics.Data.Entities;
using ShopElectronics.Data.Repositories.Interfaces;
using ShopElectronics.Services.Models.Dto;
using ShopElectronics.Services.Models.ViewModels;
using ShopElectronics.Services.Services.Interfaces;

namespace ShopElectronics.Services.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _autoMapper;

    public ShoppingCartService(ICartRepository cartRepository, IMapper autoMapper, IUserRepository userRepository)
    {
        _cartRepository = cartRepository;
        _autoMapper = autoMapper;
        _userRepository = userRepository;
    }

    public async Task<CartItemViewModel> AddItem(List<CartItemToAddDto> cartItemsToAdd)
    {
        if (string.IsNullOrEmpty(cartItemsToAdd.First().Username))
        {
            return null;
        }
        
        var user = await _userRepository.GetUser(cartItemsToAdd.First().Username, String.Empty);

        var temp = _autoMapper.Map<ICollection<CartItem>>(cartItemsToAdd);
        var addResult = await _cartRepository.AddItem(temp, user);
        
        if (!addResult) return null;
        
        var items = await _cartRepository.GetItems(user.Id);
        return _autoMapper.Map<CartItemViewModel>(items);

    }

    public async Task<CartItemViewModel> UpdateQty(CartItemToUpdDto cartItemQtyUpdateDto)
    {
        if (string.IsNullOrEmpty(cartItemQtyUpdateDto.Username))
        {
            return null;
        }
        
        var user = await _userRepository.GetUser(cartItemQtyUpdateDto.Username, String.Empty);

        var temp = await _cartRepository.UpdateQty( user.Cart.Id, cartItemQtyUpdateDto.ProductId, cartItemQtyUpdateDto.Qwt);
        return _autoMapper.Map<CartItemViewModel>(temp);
    }

    public async Task<ICollection<CartItemViewModel>> GetItems(string username)
    {
        var user = await _userRepository.GetUser(username, String.Empty);
        var temp = await _cartRepository.GetItems(user.Id);
        return temp == null ? null : _autoMapper.Map<ICollection<CartItemViewModel>>(temp);
    }
}