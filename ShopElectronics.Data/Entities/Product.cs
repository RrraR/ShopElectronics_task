using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopElectronics.Data.Entities
{
    [Table("Product")]
    public partial class Product
    {
        // public Product()
        // {
        //     CartItems = new HashSet<CartItem>();
        // }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = null!;
        [StringLength(150)]
        public string Description { get; set; } = null!;
        public string ImageURL { get; set; } = null!;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        
        
        [ForeignKey("BrandId")]
        [InverseProperty("Products")]
        public virtual Brands Brands { get; set; } = null!;
        

        [ForeignKey("CategoryId")]
        [InverseProperty("Products")]
        public virtual ProductCategory Category { get; set; } = null!;
        
        
        
        [InverseProperty("Product")]
        public virtual ICollection<CartItem> CartItems { get; set; }
        
        
        
        [InverseProperty("Product")]
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
