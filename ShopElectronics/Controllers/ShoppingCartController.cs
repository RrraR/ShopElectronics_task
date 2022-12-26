using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopElectronics.Data.Repositories.Interfaces;
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
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IProductRepository productRepository)
        {
            _shoppingCartService = shoppingCartService;
            _productRepository = productRepository;
        }
        
        [HttpGet]
        [Route("/shop/{id}/checkout")]
        [Authorize]
        public async Task<IEnumerable<CartItemViewModel>> Checkout(int userId)
        {
            return null;
            // var cartItems = await _shoppingCartService.GetItems(userId);
            // return cartItems;
        }

        [HttpGet]
        public async Task<IEnumerable<CartItemViewModel>> GetItems(int userId)
        {
            var cartItems = await _shoppingCartService.GetItems(userId);
            return cartItems;
        }

        [HttpGet("{id}")]
        public async Task<CartItemViewModel> GetItem(int id)
        {
            var cartItem = await _shoppingCartService.GetItem(id);
            return cartItem;
        }

        [HttpPost]
        public async Task<CartItemViewModel> AddItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            var newCartItem = await _shoppingCartService.AddItem(cartItemToAddDto);
            return newCartItem;
        }

        [HttpDelete("{id:int}")]
        public async Task<CartItemViewModel> DeleteItem(int id)
        {
            var cartItem = await _shoppingCartService.DeleteItem(id);
            return cartItem;
        }

        [HttpPatch("{id:int}")]
        public async Task<CartItemViewModel> UpdateQty(int id, CartItemToUpdDto cartItemToUpdDto)
        {
            var cartItem = await _shoppingCartService.UpdateQty(cartItemToUpdDto);
            return cartItem;
        }
    }
}