using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO
{
    public class UpdateMemberDTO
    {
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Email is reqired")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "CompanyName is reqired")]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "City is reqired")]
        [StringLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is reqired")]
        [StringLength(100)]
        public string Country { get; set; }
    }
}
