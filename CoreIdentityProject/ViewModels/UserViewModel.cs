using System.ComponentModel.DataAnnotations;

namespace CoreIdentityProject.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage ="Kullanıcı ismi gereklidir.")]
        [Display(Name="Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "Tel No")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [Display(Name = "Email Adresi")]
        [EmailAddress(ErrorMessage = "Email adresiniz doğru formatta değil.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifreniz  gereklidir.")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
