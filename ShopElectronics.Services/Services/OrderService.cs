using System.Collections.ObjectModel;
using AutoMapper;
using ShopElectronics.Data.Entities;
using ShopElectronics.Data.Repositories.Interfaces;
using ShopElectronics.Services.Models.Dto;
using ShopElectronics.Services.Services.Interfaces;

namespace ShopElectronics.Services.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICartRepository _cartRepository;
    

    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper, ICartRepository cartRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _cartRepository = cartRepository;
    }

    public async Task<bool> AddOrder(ICollection<OrderItemsToAddDto> OrderItemsToAdd)
    {
        var user = await _userRepository.GetUser(OrderItemsToAdd.First().Username, string.Empty);
        var order = await _orderRepository.CreateOrder(user);
        var temp = _mapper.Map<ICollection<OrderItems>>(OrderItemsToAdd);
        var addedItems = await _orderRepository.AddOrderItemsToOrder(order, temp);

        if (order == null || !order.OrderItems.Any()) return false;
        
        var result = await _cartRepository.DeleteItemsOnOrder(addedItems);
        return result;
    }

    public async Task<ICollection<OrdersDto>> GetOrders(string username)
    {
        List<Orders> result;
        if (username == "admin")
        {
            result = await _orderRepository.GetAllOrders();
        }
        else
        {
            var user = await _userRepository.GetUser(username, string.Empty);
            result = await _orderRepository.GetOrders(user);
        }
        var temp = _mapper.Map<ICollection<OrdersDto>>(result);
        return temp;
    }

    public async Task<ICollection<OrderStatusesDto>> GetAllOrderStatuses()
    {
        var res = await _orderRepository.GetAllOrderStatuese();
        return _mapper.Map<ICollection<OrderStatusesDto>>(res);
    }

    public async Task<ICollection<OrdersDto>> UpdateOrderStatuses(ICollection<OrdersToChangeDto> OrdersToChange)
    {
        foreach (var item in OrdersToChange)
        {
            var order = await _orderRepository.GetOrderById(item.OrderId);
            order.OrderStatusId = _orderRepository.GetStatusByStatusName(item.Status).Result.Id;
        } 
        
        await _orderRepository.UpdateOrders();

        var result = await GetOrders("admin");
        return result;

    }
}