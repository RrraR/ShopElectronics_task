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

        public async Task<CartItem> AddItem(CartItem cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in _shopElectronicsDbContext.Products
                    where product.Id == cartItemToAddDto.ProductId
                    select new CartItem
                    {
                        CartId = cartItemToAddDto.CartId,
                        ProductId = product.Id,
                        Qty = cartItemToAddDto.Qty
                    }).SingleOrDefaultAsync();


                if (item != null)
                {
                    var result = await _shopElectronicsDbContext.CartItems.AddAsync(item);
                    await _shopElectronicsDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return null;
        }

        public async Task<CartItem> UpdateQty(int id, int qwt)
        {
            var item = await _shopElectronicsDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Qty = qwt;
                await _shopElectronicsDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }

        public async void DeleteItem(int id)
        {
            var item = await _shopElectronicsDbContext.CartItems.FindAsync(id);
            _shopElectronicsDbContext.CartItems.Remove(item);
            await _shopElectronicsDbContext.SaveChangesAsync();
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (_shopElectronicsDbContext.CartItems.SingleOrDefaultAsync(c => c.Id == id));
        }

        public async Task<ICollection<CartItem>> GetItems(int userId)
        {
            return await (from cart in _shopElectronicsDbContext.Carts
                join cartItem in _shopElectronicsDbContext.CartItems
                    on cart.Id equals cartItem.CartId
                where cart.UserId == userId
                select new CartItem
                {
                    Id = cartItem.Id,
                    ProductId = cartItem.ProductId,
                    Qty = cartItem.Qty,
                    CartId = cartItem.CartId
                }).ToListAsync();
        }
    }
}