using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO
{
    public class ChangePasswordDTO
    {
        public int MemberId { get; set; }
        [Required(ErrorMessage = "Current Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(100, MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
