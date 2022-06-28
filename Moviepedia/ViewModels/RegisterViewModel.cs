using System.ComponentModel.DataAnnotations;

namespace Moviepedia.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage ="Invalid email")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50,MinimumLength = 6, ErrorMessage ="Password must be 6 to 50 symbols long")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Doesn't match your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password confirmation")]
        public string PasswordConfirm { get; set; }
    }
}
