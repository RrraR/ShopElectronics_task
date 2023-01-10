using ShopElectronics.Services.Models.ViewModels;

namespace ShopElectronics.Services.Services.Interfaces;

public interface IProductService
{
    Task<ProductViewModel> GetItem(int id);
    Task<ICollection<ProductViewModel>> GetItems();
    Task<ICollection<ProductViewModel>> GetItemsByCategory(int id);
}