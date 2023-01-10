using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopElectronics.Data.Entities;
using ShopElectronics.Data.Repositories;
using ShopElectronics.Services.Models.Dto;
using ShopElectronics.Services.Models.ViewModels;

namespace ShopElectronics;

public class AutoMapper: Profile
{
    public AutoMapper()
    {
        CreateMap<Product, ProductViewModel>()
            .ForMember(d=>d.ProductId, o=>o.MapFrom(s=>s.Id))
            .ForMember(d=>d.Name, o=>o.MapFrom(s=>s.Name))
            .ForMember(d=>d.Image, o=>o.MapFrom(s=>s.ImageURL))
            .ForMember(d=>d.Description, o=>o.MapFrom(s=>s.Description))
            .ForMember(d=>d.Price, o=>o.MapFrom(s=>s.Price))
            .ForMember(d=>d.CategoryId, o=>o.MapFrom(s=>s.Category.Id))
            .ForMember(d=>d.CategoryName, o=>o.MapFrom(s=>s.Category.Name))
            .ForMember(d=>d.BrandName, o=>o.MapFrom(s=>s.Brands.Name));

        CreateMap<ProductCategory, CategoryViewModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d=>d.Name, o=>o.MapFrom(s=>s.Name))
            .ForMember(d=>d.ImageUrl, o=>o.MapFrom(s=>s.ImageURL));

        CreateMap<CartItem, CartItemViewModel>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Qwt, o => o.MapFrom(s => s.Qwt))
            .ForMember(d => d.CartId, o => o.MapFrom(s => s.CartId))
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Product.Name))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Product.Price));

        CreateMap<CartItemToAddDto, CartItem>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
            .ForMember(d => d.Qwt, o => o.MapFrom(s => s.Qwt));

        CreateMap<User, UserDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName))
            .ForMember(d => d.Password, o => o.MapFrom(s => s.Password));

        CreateMap<OrderItemsToAddDto, OrderItems>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
            .ForMember(d => d.Qwt, o => o.MapFrom(s => s.Qwt));

        CreateMap<Orders, OrdersDto>()
            .ForMember(d => d.OrderId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.User.UserName))
            .ForMember(d => d.OrderStatus, o => o.MapFrom(s => s.OrderStatus.Status))
            .ForMember(d => d.OrderStatusId, o => o.MapFrom(s => s.OrderStatus.Id));
            ;

        CreateMap<OrderItems, OrderItemsDto>()
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
            .ForMember(d => d.Qwt, o => o.MapFrom(s => s.Qwt));


        CreateMap<OrderStatuses, OrderStatusesDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Status));




    }
    
}