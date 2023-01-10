using System.Net.NetworkInformation;
using AutoMapper;
using ShopElectronics.Services.Services.Interfaces;
using ShopElectronics.Data.Repositories.Interfaces;
using ShopElectronics.Services.Models.ViewModels;

namespace ShopElectronics.Services.Services;

public class ProductService : IProductService
{
    private readonly IMapper _autoMapper;
    private readonly IProductRepository _productRepositiry;

    public ProductService(IProductRepository productRepositiry, IMapper autoMapper)
    {
        _productRepositiry = productRepositiry;
        _autoMapper = autoMapper;
    }

    public async Task<ProductViewModel> GetItem(int id)
    {
        var product = await _productRepositiry.GetItem(id);
        return _autoMapper.Map<ProductViewModel>(product);
    }

    public async Task<ICollection<ProductViewModel>> GetItems()
    {
        var products = await _productRepositiry.GetItems();

        return _autoMapper.Map<ICollection<ProductViewModel>>(products);
    }

    public async Task<ICollection<ProductViewModel>> GetItemsByCategory(int id)
    {
        var products = await _productRepositiry.GetItemsByCategory(id);
        return _autoMapper.Map<ICollection<ProductViewModel>>(products);
    }
}