using Microsoft.AspNetCore.Mvc;
using ShopElectronics.Services.Models.Dto;
using ShopElectronics.Services.Services.Interfaces;

namespace ShopElectronics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpPost]
        [Route("getOrders")]
        public async Task<ICollection<OrdersDto>> GetAllOrdersForUser([FromBody]UsernameDto username)
        {
            var result = await _orderService.GetOrders(username.Username);
            return result;
        }
        
        [HttpPost]
        [Route("adminGetOrders")]
        public async Task<ICollection<OrdersDto>> GetAllOrdersForAdmin()
        {
            var result = await _orderService.GetOrders("admin");
            return result;
        }
        
        [HttpGet]
        public async Task<ICollection<OrderStatusesDto>> GetOrderStatuses()
        {
            var result = await _orderService.GetAllOrderStatuses();
            return result;
        }
        
        
        [HttpPost]
        [Route("adminUpdateOrders")]
        public async Task<ICollection<OrdersDto>> AdminUpdateOrders([FromBody] List<OrdersToChangeDto> ordersToChangeList)
        {
            var result = await _orderService.UpdateOrderStatuses(ordersToChangeList);
            return result;
        }
    }
}
