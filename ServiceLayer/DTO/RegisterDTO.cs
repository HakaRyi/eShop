using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(100, MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(200, ErrorMessage = "Company name can't be longer than 200 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City name can't be longer than 100 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, ErrorMessage = "Country name can't be longer than 100 characters")]
        public string Country { get; set; }
    }
}
