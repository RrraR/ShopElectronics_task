namespace ShopElectronics.Services.Models.Dto;

public class CartItemToAddDto
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Qty { get; set; }
}