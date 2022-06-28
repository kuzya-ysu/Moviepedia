using System.ComponentModel.DataAnnotations;

namespace Moviepedia.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Doesn't match your new password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password confirmation")]
        public string PasswordConfirm { get; set; }
    }
}
