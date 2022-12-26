using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopElectronics.Data.Entities
{
    [Table("Cart")]
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Cart")]
        public virtual User User { get; set; } = null!;
        [InverseProperty("Cart")]
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
