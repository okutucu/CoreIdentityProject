using System.Threading.Tasks;
using CoreIdentityProject.Models;
using CoreIdentityProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentityProject.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  LogIn(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if(user != null)
                {
                    await _signInManager.SignOutAsync();
                   Microsoft.AspNetCore.Identity.SignInResult result =  await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Member");
                    }
                   
                }
            }

            else
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifresi");
            }




            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
          
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  SignUp(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;

                IdentityResult result =  await _userManager.CreateAsync(user,userViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach(IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }

            }
            return View(userViewModel);
        }

    }
}
