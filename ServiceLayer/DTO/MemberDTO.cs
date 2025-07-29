using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTO
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
