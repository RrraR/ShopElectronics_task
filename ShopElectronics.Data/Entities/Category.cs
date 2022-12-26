using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopElectronics.Data.Entities;

[Table("Categories")]
public class Category
{
    public Category()
    {
        ProductCategories = new HashSet<ProductCategory>();
    }

    [Key]
    public int Id { get; set; }
    
    [StringLength(50)]
    public string CategoryKey { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<ProductCategory> ProductCategories { get; set; }
}