using Microsoft.EntityFrameworkCore;
using ShopElectronics.Data.Entities;
using ShopElectronics.Data.Repositories.Interfaces;

// using ShopElectronics.Services.Models.Dtos;

namespace ShopElectronics.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ShopElectronicsDbContext _shopElectronicsDbContext;

        public CartRepository(ShopElectronicsDbContext shopElectronicsDbContext)
        {
            _shopElectronicsDbContext = shopElectronicsDbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await _shopElectronicsDbContext.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                           c.ProductId == productId);
        }

        public async Task<bool> AddItem(ICollection<CartItem> cartItemToAddDto, User user)
        {
            foreach (var item in cartItemToAddDto)
            {
                item.CartId = user.Cart.Id;
            }

            foreach (var item in cartItemToAddDto)
            {
                if (await CartItemExists(item.CartId, item.ProductId) == false)
                {
                    var temp = await (from product in _shopElectronicsDbContext.Products
                        where product.Id == item.ProductId
                        select new CartItem
                        {
                            CartId = item.CartId,
                            ProductId = product.Id,
                            Qwt = item.Qwt
                        }).SingleOrDefaultAsync();

                    if (temp != null)
                    { 
                        _shopElectronicsDbContext.CartItems.Add(item);
                    }
                }
                else
                {
                    var temp = await _shopElectronicsDbContext.CartItems.FirstOrDefaultAsync(p =>
                        p.CartId == item.CartId
                        && p.ProductId == item.ProductId);
                    temp.Qwt = temp.Qwt + item.Qwt;
                }
            }
            
            await _shopElectronicsDbContext.SaveChangesAsync();
            var result = await GetItems(user.Id);
            return result.Any();
        }

        public async Task<CartItem> UpdateQty(int cartid, int productId, int qwt)
        {
            var item = await _shopElectronicsDbContext.CartItems.FirstOrDefaultAsync(p =>
                p.CartId == cartid
                && p.ProductId == productId);

            if (item == null) return null;

            if (item.Qwt == 1)
            {
                _shopElectronicsDbContext.CartItems.Remove(item);
                await _shopElectronicsDbContext.SaveChangesAsync();
                // DeleteItem(item.Id);
            }
            else
            {
                item.Qwt = item.Qwt - qwt;
                await _shopElectronicsDbContext.SaveChangesAsync();
                return await _shopElectronicsDbContext.CartItems.FirstOrDefaultAsync(p =>
                    p.CartId == cartid
                    && p.ProductId == productId);
            }

            return null;
        }

        public async Task<bool> DeleteItemsOnOrder(ICollection<OrderItems> ItemsToDelete)
        {
            var cartItemsToRemove =
                await _shopElectronicsDbContext.CartItems.Where(i =>
                    i.Cart.UserId == ItemsToDelete.First().Order.UserId).ToListAsync();
            foreach (var item in cartItemsToRemove)
            {
                _shopElectronicsDbContext.CartItems.Remove(item);
            }

            await _shopElectronicsDbContext.SaveChangesAsync();

            return !_shopElectronicsDbContext.CartItems.Any(i => i.Cart.UserId == ItemsToDelete.First().Order.UserId);
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (_shopElectronicsDbContext.CartItems.SingleOrDefaultAsync(c => c.Id == id));
        }

        public async Task<ICollection<CartItem>> GetItems(int userId)
        {
            return await _shopElectronicsDbContext.CartItems.Include(i => i.Cart).ThenInclude(p => p.User)
                .Where(u => u.Cart.UserId == userId).ToListAsync();
        }
    }
}