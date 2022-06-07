using System.ComponentModel.DataAnnotations;

namespace CoreIdentityProject.ViewModels
{
    public class PasswordResetViewModel
    {
        [Required(ErrorMessage = "Email alanı gereklidir.")]
        [EmailAddress]
        [Display(Name = "Email Adresiniz")]
        public string Email { get; set; }
    }
}
