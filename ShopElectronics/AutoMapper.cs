using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopElectronics.Data.Entities;
using ShopElectronics.Services.Models.Dto;
using ShopElectronics.Services.Models.ViewModels;

namespace ShopElectronics;

public class AutoMapper: Profile
{
    public AutoMapper()
    {
        CreateMap<Product, ProductViewModel>()
            .ForMember(d=>d.Id, o=>o.MapFrom(s=>s.Id))
            .ForMember(d=>d.Name, o=>o.MapFrom(s=>s.Name))
            .ForMember(d=>d.Image, o=>o.MapFrom(s=>s.ImageURL))
            .ForMember(d=>d.Price, o=>o.MapFrom(s=>s.Price))
            .ForMember(d=>d.CategoryId, o=>o.MapFrom(s=>s.Category.Id))
            .ForMember(d=>d.CategoryName, o=>o.MapFrom(s=>s.Category.Name))
            .ForMember(d=>d.BrandName, o=>o.MapFrom(s=>s.BrandName));

        CreateMap<ProductCategory, CategoryViewModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d=>d.Name, o=>o.MapFrom(s=>s.Name))
            .ForMember(d=>d.ImageUrl, o=>o.MapFrom(s=>s.ImageURL));

        CreateMap<CartItem, CartItemViewModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Product.Price))
            .ForMember(d => d.Qty, o => o.MapFrom(s => s.Qty))
            .ForMember(d => d.CartId, o => o.MapFrom(s => s.CartId))
            .ForMember(d => d.ProductDescription, o => o.MapFrom(s => s.Product.Description))
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
            .ForMember(d => d.ProductImageURL, o => o.MapFrom(s => s.Product.ImageURL));

        CreateMap<User, UserDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName))
            .ForMember(d => d.Password, o => o.MapFrom(s => s.Password));



    }
    
}