using System.ComponentModel.DataAnnotations;

namespace CoreIdentityProject.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name ="Rol ismi")]
        [Required(ErrorMessage ="Rol ismi gereklidir")]
        public string Name { get; set; }

        public string Id { get; set; }
        

    }
}
