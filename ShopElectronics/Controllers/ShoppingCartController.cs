using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopElectronics.Services.Models.Dto;
using ShopElectronics.Services.Models.ViewModels;
using ShopElectronics.Services.Services.Interfaces;

namespace ShopElectronics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IOrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }
        
        [HttpPost]
        [Route("shop/checkout")]
        public async Task<IActionResult> Checkout([FromBody]List<OrderItemsToAddDto> OrderItems)
        {
            var res = await _orderService.AddOrder(OrderItems);
            if (res)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("shop/getitems")]
        [AllowAnonymous]
        public async Task<IEnumerable<CartItemViewModel>> GetItemsInCart([FromBody] UsernameDto username)
        {
            if (string.IsNullOrEmpty(username.Username)) return null;
            
            var cartItems = await _shoppingCartService.GetItems(username.Username);
            return !cartItems.Any() ? null : cartItems;
        }

        [HttpPost]
        [Route("shop/additem")]
        [AllowAnonymous]
        public async Task<CartItemViewModel> addItems([FromBody] List<CartItemToAddDto> cartItemToAddDto)
        {
            var newCartItem = await _shoppingCartService.AddItem(cartItemToAddDto);
            return newCartItem;
        }

        [HttpPatch]
        [AllowAnonymous]
        public async Task<CartItemViewModel> UpdateQty([FromBody]CartItemToUpdDto cartItemToUpdDto)
        {
            var cartItem = await _shoppingCartService.UpdateQty(cartItemToUpdDto);
            return cartItem;
        }
    }
}