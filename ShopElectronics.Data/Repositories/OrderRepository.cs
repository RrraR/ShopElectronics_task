using Microsoft.EntityFrameworkCore;
using ShopElectronics.Data.Entities;
using ShopElectronics.Data.Repositories.Interfaces;

namespace ShopElectronics.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShopElectronicsDbContext _shopElectronicsDbContext;

    public OrderRepository(ShopElectronicsDbContext shopElectronicsDbContext)
    {
        _shopElectronicsDbContext = shopElectronicsDbContext;
    }

    public async Task<List<Orders>> GetOrders(User user)
    {
        return await _shopElectronicsDbContext.Orders.Where(o => o.UserId == user.Id).ToListAsync();
    }

    public async Task<List<Orders>> GetAllOrders()
    {
        return await _shopElectronicsDbContext.Orders.ToListAsync();
    }

    public async Task<ICollection<OrderItems>> AddOrderItemsToOrder(Orders order, ICollection<OrderItems> OrderItems)
    {
        foreach (var item in OrderItems)
        {
            item.OrderId = order.Id;
            await _shopElectronicsDbContext.OrderItems.AddAsync(item);
        }

        await _shopElectronicsDbContext.SaveChangesAsync();
        
        return OrderItems;

    }

    public async Task<Orders> CreateOrder(User user)
    {
        var order = new Orders()
        {
            UserId = user.Id,
            OrderStatusId = 1
        };
        await _shopElectronicsDbContext.Orders.AddAsync(order);
        await _shopElectronicsDbContext.SaveChangesAsync();
        return order;
    }

    public async Task<ICollection<OrderStatuses>> GetAllOrderStatuese()
    {
        return await _shopElectronicsDbContext.OrderStatuses.ToListAsync();
    }

    public async Task<bool> UpdateOrders()
    {
        try
        {
            await _shopElectronicsDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return false;

    }

    public async Task<Orders> GetOrderById(int OrderId)
    {
        return await _shopElectronicsDbContext.Orders.FirstOrDefaultAsync(o => o.Id == OrderId);
    }

    public Task<OrderStatuses> GetStatusByStatusName(string statusName)
    {
        return _shopElectronicsDbContext.OrderStatuses.FirstOrDefaultAsync(s => s.Status == statusName);
    }
}