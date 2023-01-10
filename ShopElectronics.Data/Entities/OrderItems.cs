using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopElectronics.Data.Entities;

[Table("OrderItems")]
public class OrderItems
{
    [Key] 
    public int Id { get; set; }
    
    public int Qwt { get; set; }

    public int ProductId { get; set; }

    public int OrderId { get; set; }
    
    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Orders Order { get; set; } = null!;
    
    [ForeignKey("ProductId")]
    [InverseProperty("OrderItems")]
    public virtual Product Product { get; set; } = null!;
}