using System.ComponentModel.DataAnnotations;

namespace CoreIdentityProject.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email alanı gereklidir.")]
        [EmailAddress]
        [Display(Name ="Email Adresiniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        [MinLength(4,ErrorMessage ="Şifreniz en az 4 karakterli olmalıdır.")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
