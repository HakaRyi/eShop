using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class ProductResponse
    {
        public List<ProductDTO> Products { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
    }
}
