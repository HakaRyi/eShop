using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.Entities;

namespace ServiceLayer.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; } 
        public DateTime? ShippedDate { get; set; } 
        public decimal? Freight { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; } = new();
    }
}
