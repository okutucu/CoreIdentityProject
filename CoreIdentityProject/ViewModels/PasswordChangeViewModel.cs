using System.ComponentModel.DataAnnotations;

namespace CoreIdentityProject.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Display(Name = "Eski Şifreniz")]
        [Required(ErrorMessage ="Eski şifreniz gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage ="Şifreniz en az 4 karakterli olmak zorundadır.")]
        public string PasswordOld { get; set; }

        [Display(Name = "Yeni Şifreniz")]
        [Required(ErrorMessage = "Yeni şifreniz gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmak zorundadır.")]
        public string PasswordNew { get; set; }


        [Display(Name = "Tekrar Yeni Şifreniz")]
        [Required(ErrorMessage = "Yeni şifre onay gereklidir")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Şifreniz en az 4 karakterli olmak zorundadır.")]
        [Compare("PasswordNew",ErrorMessage ="Yeni şifreniz ve onay şifreniz birbirinden farklıdır..")]
        public string PasswordConfirm { get; set; }
    }
}
