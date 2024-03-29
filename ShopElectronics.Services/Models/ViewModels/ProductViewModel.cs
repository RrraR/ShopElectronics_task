﻿namespace ShopElectronics.Services.Models.ViewModels;

public class ProductViewModel
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string BrandName { get; set; }
}