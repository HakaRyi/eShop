using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTO
{
    public class LoginDTO
    {
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
