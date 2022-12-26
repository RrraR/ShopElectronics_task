using ShopElectronics.Services.Models.Dto;
using ShopElectronics.Services.Models.ViewModels;

namespace ShopElectronics.Services.Services.Interfaces;

public interface IShoppingCartService
{
    Task<CartItemViewModel> AddItem(CartItemToAddDto cartItemToAddDto);
    Task<CartItemViewModel> UpdateQty(CartItemToUpdDto cartItemQtyUpdateDto);
    Task<CartItemViewModel> DeleteItem(int id);
    Task<CartItemViewModel> GetItem(int id);
    Task<ICollection<CartItemViewModel>> GetItems(int userId);
}