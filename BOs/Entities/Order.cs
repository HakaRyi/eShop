using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.Entities
{
    public enum OrderStatus
    {
        Pending=0,    
        Success=1    
    }
    public class Order
    {
        [Key]
        [Column("OrderId")]
        public int OrderId { get; set; }

        [Column("MemberId")]
        public int MemberId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? RequiredDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ShippedDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }
        [Column("Status")]
        public OrderStatus Status { get; set; }
        // Navigation property
        [ForeignKey("MemberId")]
        [InverseProperty("Orders")]
        public virtual Member Member { get; set; } = null!;

        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        [InverseProperty("Order")]
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
