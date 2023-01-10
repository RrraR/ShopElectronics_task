using ShopElectronics.Data.Entities;

namespace ShopElectronics.Services.Models.Dto;

public class OrdersDto
{
    public string Username { get; set; }
    public int OrderId { get; set; }
    public int OrderStatusId { get; set; }
    public string OrderStatus { get; set; }

    public ICollection<OrderItemsDto> OrderItems { get; set; }

}