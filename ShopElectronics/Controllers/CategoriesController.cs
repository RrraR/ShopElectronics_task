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

        public CategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
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