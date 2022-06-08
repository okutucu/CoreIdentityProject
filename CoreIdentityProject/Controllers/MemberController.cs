using System.Threading.Tasks;
using CoreIdentityProject.Models;
using CoreIdentityProject.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentityProject.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {

            AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            UserViewModel userViewModel = user.Adapt<UserViewModel>();
            

            return View(userViewModel);
        }

        public IActionResult UserEdit()
        {
            AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            UserViewModel userViewModel = user.Adapt<UserViewModel>();

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult>  UserEdit(UserViewModel userViewModel)
        {
            ModelState.Remove("Password");
           if(ModelState.IsValid)
           {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if(result.Succeeded)
                {
                   await _userManager.UpdateSecurityStampAsync(user);
                   await  _signInManager.SignOutAsync();
                   await _signInManager.SignInAsync(user,true);




                    ViewBag.Success = "true";
                }
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
           }

            return View(userViewModel);
        }

        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordChange(PasswordChangeViewModel passwordChangeViewModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;

                bool exist = _userManager.CheckPasswordAsync(user, passwordChangeViewModel.PasswordOld).Result;

                if (user!= null)
                {
                   

                    if(exist)
                    {
                        IdentityResult result = _userManager.ChangePasswordAsync(user, passwordChangeViewModel.PasswordOld, passwordChangeViewModel.PasswordNew).Result;

                        if(result.Succeeded)
                        {
                            _userManager.UpdateSecurityStampAsync(user);
                            _signInManager.SignOutAsync();
                            _signInManager.PasswordSignInAsync(user, passwordChangeViewModel.PasswordNew, true, false);

                            ViewBag.Success = "true";
                        }
                        else
                        {
                            foreach (IdentityError error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Eski şifreniz yanlış");
                    }
                }
            }
            return View(passwordChangeViewModel);
        }

        public void LogOut()
        {
            _signInManager.SignOutAsync();
            
        }
    }
}
