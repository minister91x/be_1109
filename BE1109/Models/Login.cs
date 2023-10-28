using System.ComponentModel.DataAnnotations;

namespace BE1109.Models
{
    public class Login
    {
    }

    public class LoginModel
    {

        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }


    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

    public class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

}
