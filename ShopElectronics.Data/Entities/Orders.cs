using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopElectronics.Data.Entities;

[Table("Orders")]
public partial class Orders
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int OrderStatusId { get; set; }
    
    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; } = null!;
    
    [ForeignKey("OrderStatusId")]
    [InverseProperty("Orders")]
    public virtual OrderStatuses OrderStatus { get; set; } = null!;
    
    [InverseProperty("Order")]
    public virtual ICollection<OrderItems> OrderItems { get; set; }
    
}