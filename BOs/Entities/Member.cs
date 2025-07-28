using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.Entities
{
   
    public class Member
    {
        [Required]
        public int MemberId { get; set; }
        [Required (ErrorMessage ="Email is reqired")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is reqired")]
        [StringLength(100)]
        public string Password { get; set; }
        [Required(ErrorMessage = "CompanyName is reqired")]
        [StringLength(200)]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "City is reqired")]
        [StringLength(100)]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is reqired")]
        [StringLength(100)]
        public string Country { get; set; }
        public bool Status { get; set; } = true;
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Feedbacks> Feedbacks { get; set; } = new List<Feedbacks>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
