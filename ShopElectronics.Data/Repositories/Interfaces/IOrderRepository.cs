using ShopElectronics.Data.Entities;

namespace ShopElectronics.Data.Repositories.Interfaces;

public interface IOrderRepository
{
    public Task<List<Orders>> GetOrders(User user);
    public Task<List<Orders>> GetAllOrders();
    public Task<ICollection<OrderItems>> AddOrderItemsToOrder(Orders order, ICollection<OrderItems> OrderItems);
    public Task<Orders> CreateOrder(User user);
    public Task<ICollection<OrderStatuses>> GetAllOrderStatuese();
    public Task<bool> UpdateOrders();
    public Task<Orders> GetOrderById(int OrderId);
    public Task<OrderStatuses> GetStatusByStatusName(string statusName);
}