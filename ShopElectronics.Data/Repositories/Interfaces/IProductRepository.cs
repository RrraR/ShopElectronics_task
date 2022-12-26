using ShopElectronics.Data.Entities;

namespace ShopElectronics.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetItems();
        Task<Product> GetItem(int id);
        Task<ICollection<Product>> GetItemsByCategory(int id);

    }
}
