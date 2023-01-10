namespace ShopElectronics.Services.Models.ViewModels;

public class CartItemViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Qwt { get; set; }
}