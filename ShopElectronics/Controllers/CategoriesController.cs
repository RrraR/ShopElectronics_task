using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopElectronics.Services.Models.ViewModels;
using ShopElectronics.Services.Services.Interfaces;

namespace ShopElectronics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        public CategoriesController(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ICollection<CategoryViewModel>> GetProductCategories(int id)
        {
            var categories = await _productCategoryService.GetCategories(id);
            return categories;
        }
    }
}