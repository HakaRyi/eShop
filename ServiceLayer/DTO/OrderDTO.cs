using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class OrderDTO
    {
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<CartItemDTO> Items { get; set; }
    }
}
