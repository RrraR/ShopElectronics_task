using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShopElectronics.Data.Entities
{
    [Table("User")]
    public partial class User
    {
        // public User()
        // {
        //     Carts = new HashSet<Cart>();
        // }

        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string UserName { get; set; } = null!;
        
        [StringLength(30)]
        public string Password { get; set; } = null!;

        [InverseProperty("User")]
        public virtual Cart Cart { get; set; }
    }
}
