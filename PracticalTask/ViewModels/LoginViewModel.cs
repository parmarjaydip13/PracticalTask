using System.ComponentModel.DataAnnotations;

namespace PracticalTask
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter an valid email address.")]
        [MaxLength(length:50, ErrorMessage = "Email address is too long")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }

    }
}
