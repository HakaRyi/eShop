using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        [Key, Column(Order = 0)]
        public int OrderId { get; set; }

        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public float Discount { get; set; }

        // Navigation properties
        [ForeignKey("OrderId")]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; } = null!;

        [ForeignKey("ProductId")]
        [InverseProperty("OrderDetails")]
        public virtual Product Product { get; set; } = null!;
    }
}
