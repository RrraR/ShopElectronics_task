using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopElectronics.Data.Entities;

[Table("OrderStatus")]
public partial class OrderStatuses
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Status { get; set; }
    
    [InverseProperty("OrderStatus")]
    public virtual ICollection<Orders> Orders { get; set; }
}