using ShopElectronics.Services.Models.ViewModels;

namespace ShopElectronics.Services.Services.Interfaces;

public interface IProductService
{
    Task<ProductViewModel> GetItem(int id);
    Task<ICollection<ProductViewModel>> GetItems();
    
    //Task<ProductViewModel> GetCategory(int id);
    // Task<ICollection<CategoryViewModel>> GetCategories();
    Task<ICollection<ProductViewModel>> GetItemsByCategory(int id);
}