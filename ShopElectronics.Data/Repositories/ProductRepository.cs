using Microsoft.EntityFrameworkCore;
using ShopElectronics.Data.Entities;
using ShopElectronics.Data.Repositories.Interfaces;

namespace ShopElectronics.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopElectronicsDbContext _shopElectronicsDbContext;

        public ProductRepository(ShopElectronicsDbContext shopElectronicsDbContext)
        {
            _shopElectronicsDbContext = shopElectronicsDbContext;
        }

        public async Task<Product> GetItem(int id)
        {
            // var product = await _shopElectronicsDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            var product = await _shopElectronicsDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<ICollection<Product>> GetItems()
        {
            var products = await _shopElectronicsDbContext.Products.ToListAsync();
            
            return products;
        }
        
        public async Task<ICollection<Product>> GetItemsByCategory(int id)
        {
            var products = await _shopElectronicsDbContext.Products
                .Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }
    }
}