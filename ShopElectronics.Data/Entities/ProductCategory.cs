using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopElectronics.Data.Entities
{
    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public int CategoryKeyId { get; set; }
        
        public string ImageURL { get; set; } = null!;
        

        [InverseProperty("Category")]
        public virtual ICollection<Product> Products { get; set; }
        
        
        [ForeignKey("CategoryKeyId")]
        [InverseProperty("ProductCategories")]
        public virtual Category Category { get; set; } = null!;
    }
}
