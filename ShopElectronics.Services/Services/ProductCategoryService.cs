using AutoMapper;
using ShopElectronics.Data.Repositories.Interfaces;
using ShopElectronics.Services.Models.ViewModels;
using ShopElectronics.Services.Services.Interfaces;

namespace ShopElectronics.Services.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IMapper _mapper;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = productCategoryRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<CategoryViewModel>> GetCategories(int id)
    {
        var categories = await _productCategoryRepository.GetCategories(id);
        
        return _mapper.Map<ICollection<CategoryViewModel>>(categories);
    }
    
}