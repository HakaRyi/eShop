using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.Entities
{
    public class Transaction
    {
        [Key]
        [Column("TransactionId")]
        public int TransactionId { get; set; }

        [Column("OrderId")]
        public int OrderId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [StringLength(20)]
        public string? Status { get; set; }

        [StringLength(255)]
        public string? Note { get; set; }

        // Quan hệ: mỗi transaction gắn với một order
        [ForeignKey("OrderId")]
        [InverseProperty("Transactions")]
        public virtual Order Order { get; set; } = null!;
    }
}
