using System.ComponentModel.DataAnnotations;

namespace M1MartAPI.Auth.AuthDtos
{
    public class RegisterRequestDto
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "{0} must be filled")]
        public string Username { get; set; } = null!;

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} must be filled")]
        public string Password { get; set; } = null!;

        [Display(Name = "Role")]
        [Required(ErrorMessage = "{0} must be filled")]
        public string Role { get; set; } = null!;

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} must be filled")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} must be filled")]
        public string Email { get; set; } = null!;
    }
}
