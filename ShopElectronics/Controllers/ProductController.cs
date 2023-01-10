using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopElectronics.Services.Models.ViewModels;
using ShopElectronics.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ShopElectronics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ICollection<ProductViewModel>> GetItems()
        {
            var products = await _productService.GetItems();
            return products;
        }
        
        
        // [HttpGet]
        // [Route("getBrands")]
        // [AllowAnonymous]
        // public async Task<ICollection<ProductViewModel>> GetAllBrands()
        // {
        //     var products = await _productService.GetItems();
        //     return products;
        // }


        [HttpGet]
        [Route("/shop/{id}")]
        [AllowAnonymous]
        public async Task<ProductViewModel> GetItem(int id)
        {
            var product = await _productService.GetItem(id);
            return product;
        }
        

        [HttpGet]
        // [Route("category/{id}/products$brandName={$brandname}&minPrice={minprice}&maxPrice={maxprice}")]
        [Route("category/{id}/products")]
        [AllowAnonymous]
        public async Task<ICollection<ProductViewModel>> GetItemsByCategory(int id)//, string? brandname, string? minprice, string maxprice)
        {
            var products = await _productService.GetItemsByCategory(id);
            return products;
        }
    }
}