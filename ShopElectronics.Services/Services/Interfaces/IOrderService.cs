using ShopElectronics.Services.Models.Dto;

namespace ShopElectronics.Services.Services.Interfaces;

public interface IOrderService
{
    Task<bool> AddOrder (ICollection<OrderItemsToAddDto> orders);

    Task<ICollection<OrdersDto>> GetOrders(string username);

    Task<ICollection<OrderStatusesDto>> GetAllOrderStatuses();

    Task<ICollection<OrdersDto>> UpdateOrderStatuses(ICollection<OrdersToChangeDto> ordersToChange);
}