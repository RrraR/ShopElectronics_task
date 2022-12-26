using ShopElectronics.Data.Entities;

namespace ShopElectronics.Data.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<CartItem> AddItem(CartItem cartItemToAddDto);
        Task<CartItem> UpdateQty(int id, int qwt);
        void DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<ICollection<CartItem>> GetItems(int userId);

    }
}
