using Microsoft.EntityFrameworkCore;
using ShopElectronics.Data.Entities;
using ShopElectronics.Data.Repositories.Interfaces;

namespace ShopElectronics.Data.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly ShopElectronicsDbContext _shopElectronicsDbContext;

    public ProductCategoryRepository(ShopElectronicsDbContext shopElectronicsDbContext)
    {
        _shopElectronicsDbContext = shopElectronicsDbContext;
    }

    public async Task<IEnumerable<ProductCategory>> GetCategories(int id)
    {
        switch (id)
        {
            case 0:
                var AllCategories = await _shopElectronicsDbContext.ProductCategories.ToListAsync();
                return AllCategories;
            case 1:
                var MobileCategories = _shopElectronicsDbContext.ProductCategories.Where(c=>c.CategoryKeyId == 1).ToList();
                return MobileCategories;
            case 2:
                var ComputerCategories = _shopElectronicsDbContext.ProductCategories.Where(c=>c.CategoryKeyId == 2).ToList();
                return ComputerCategories;
            default:
                return null;
        }
    }
}