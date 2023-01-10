using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopElectronics.Data.Entities;

public class Brands
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; }
    
    [InverseProperty("Brands")]
    public virtual ICollection<Product> Products { get; set; }
}