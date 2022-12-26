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
    private readonly IMapper _autoMapper;

    public ShoppingCartService(ICartRepository cartRepository, IMapper autoMapper)
    {
        _cartRepository = cartRepository;
        _autoMapper = autoMapper;
    }

    public async Task<CartItemViewModel> AddItem(CartItemToAddDto cartItemToAddDto)
    {
        var temp = new CartItem()
        {
            CartId = cartItemToAddDto.CartId,
            Qty = cartItemToAddDto.Qty,
            ProductId = cartItemToAddDto.ProductId
        };
        var item = await _cartRepository.AddItem(temp);

        return _autoMapper.Map<CartItemViewModel>(item);
    }

    public async Task<CartItemViewModel> UpdateQty(CartItemToUpdDto cartItemQtyUpdateDto)
    {
        var temp = await _cartRepository.UpdateQty(cartItemQtyUpdateDto.CartItemId, cartItemQtyUpdateDto.Qty);
        return _autoMapper.Map<CartItemViewModel>(temp);
    }

    public Task<CartItemViewModel> DeleteItem(int id)
    {
        _cartRepository.DeleteItem(id);
        return null;
    }

    public async Task<CartItemViewModel> GetItem(int id)
    {
        var temp = await _cartRepository.GetItem(id);
        return _autoMapper.Map<CartItemViewModel>(temp);
    }

    public async Task<ICollection<CartItemViewModel>> GetItems(int userId)
    {
        var temp = await _cartRepository.GetItems(userId);
        return _autoMapper.Map<ICollection<CartItemViewModel>>(temp);
    }
}