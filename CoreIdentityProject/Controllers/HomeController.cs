using System;
using System.Threading.Tasks;
using CoreIdentityProject.Models;
using CoreIdentityProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentityProject.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(userManager,signInManager)
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogIn(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
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
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError("", "Hesabınız bir süreliğine kitlenmiştir. Lütfen daha sonra tekrar deneyiniz.");


                        return View(loginViewModel);
                    }

                    if(_userManager.IsEmailConfirmedAsync(user).Result == false)
                    {
                        ModelState.AddModelError("", "Email adresiniz onaylanmamıştır. Lütfen epostanızı kontrol ediniz.");
                        return View(loginViewModel);
                    }

                   await _signInManager.SignOutAsync();
                   Microsoft.AspNetCore.Identity.SignInResult result =  await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);

                    if(result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        if(TempData["ReturnUrl"]!=null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }

                        return RedirectToAction("Index", "Member");
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);

                       

                        int fail = await _userManager.GetAccessFailedCountAsync(user);
                        ModelState.AddModelError("", $"{fail} kez başarısız giriş.");

                        if (fail ==3)
                        {
                            await _userManager.SetLockoutEndDateAsync(user,new System.DateTimeOffset(DateTime.Now.AddMinutes(20)));
                            ModelState.AddModelError("", "Hesabınız 3 başarısız giriş yapıldığından dolayı 20 dakika süreyle kitlenmiştir.Lütfen daha sonra tekrar deneyiniz.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Kullanıcı adı veya şifresi yanlış.");
                        }

                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", "Bu email adresine kayıtlı kullanıcı bulunamamıştır");
                }

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
                    string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string link = Url.Action("ConfirmEmail","Home", new
                    {
                        userId = user.Id,
                        token = confirmationToken
                    },protocol:HttpContext.Request.Scheme);

                    Helper.EmailConfirmation.SendEmail(link, user.Email);


                    ViewBag.Status = "success";


                  
                }
                else
                {
                    AddModelError(result);
                }

            }
            return View(userViewModel);
        }


        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(PasswordResetViewModel passwordResetViewModel)
        {
            AppUser user = _userManager.FindByEmailAsync(passwordResetViewModel.Email).Result;

            if(user !=null)
            {
                string passwordResetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;


                string passwordResetLink = Url.Action("ResetPasswordConfirm","Home",new
                {
                    userId = user.Id,
                    token = passwordResetToken
                },HttpContext.Request.Scheme);

                Helper.PasswordReset.PasswordResetSendEmail(passwordResetLink,user.Email,user.UserName);


                ViewBag.Status = "Success";
            }
            else
            {
                ModelState.AddModelError("", "Sistemde kayıtlı email adresi bulunamamıştır");
            }

            return View(passwordResetViewModel);
        }

        public IActionResult ResetPasswordConfirm(string userId,string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm([Bind("PasswordNew")]PasswordResetViewModel passwordResetViewModel)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if(user !=null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, passwordResetViewModel.PasswordNew);

                if(result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
               
                    ViewBag.Status = "success";
                }
                else
                {
                    AddModelError(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Hata meydana gelmiştir.Lütfen daha sonra tekrar deneyiniz.");
            }

             return View(passwordResetViewModel);
        }


        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {

            AppUser user = await _userManager.FindByIdAsync(userId);

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);

            if(result.Succeeded)
            {
                ViewBag.Status = "success";
            }
            else
            {
                ViewBag.Status = "notSuccess";
            }
           

            return View();
            
        }
    }
}
