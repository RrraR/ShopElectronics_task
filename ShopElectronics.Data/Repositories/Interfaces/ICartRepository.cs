using ShopElectronics.Data.Entities;

namespace ShopElectronics.Data.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<bool> AddItem(ICollection<CartItem> cartItemToAddDto, User user);
        public Task<CartItem> UpdateQty(int cartid, int productId, int qwt);
        Task<bool> DeleteItemsOnOrder(ICollection<OrderItems> ItemsToDelete);
        Task<CartItem> GetItem(int id);
        Task<ICollection<CartItem>> GetItems(int userId);

    }
}
