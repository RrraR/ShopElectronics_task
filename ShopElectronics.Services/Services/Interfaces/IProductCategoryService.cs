using ShopElectronics.Services.Models.ViewModels;

namespace ShopElectronics.Services.Services.Interfaces;

public interface IProductCategoryService
{
    Task<ICollection<CategoryViewModel>> GetCategories(int id);
    
    // Task<ProductCategory> GetCategory(int id);
}