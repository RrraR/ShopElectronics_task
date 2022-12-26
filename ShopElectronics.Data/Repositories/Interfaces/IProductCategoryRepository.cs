using ShopElectronics.Data.Entities;

namespace ShopElectronics.Data.Repositories.Interfaces;

public interface IProductCategoryRepository
{
    Task<IEnumerable<ProductCategory>> GetCategories(int id);
}