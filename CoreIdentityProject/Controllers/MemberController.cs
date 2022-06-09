using System;
using System.IO;
using System.Threading.Tasks;
using CoreIdentityProject.Enums;
using CoreIdentityProject.Models;
using CoreIdentityProject.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreIdentityProject.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {
       

        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager):base(userManager, signInManager)
        {
           
        }

        public IActionResult Index()
        {

            AppUser user = CurrentUser;

            UserViewModel userViewModel = user.Adapt<UserViewModel>();
            

            return View(userViewModel);
        }

        public IActionResult UserEdit()
        {
            AppUser user = CurrentUser;


            UserViewModel userViewModel = user.Adapt<UserViewModel>();

            ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult>  UserEdit(UserViewModel userViewModel,IFormFile userPicture)
        {
           ModelState.Remove("Password");
           if(ModelState.IsValid)
           {

                AppUser user =  CurrentUser;
                ViewBag.Gender = new SelectList(Enum.GetNames(typeof(Gender)));

                if (userPicture!=null && userPicture.Length>0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserPicture", fileName);


                    using(FileStream stream = new FileStream(path,FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);
                        user.Picture = "/UserPicture/" + fileName;  
                    }
                }


                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.City = userViewModel.City;
                user.BirthDay = userViewModel.BirthDay;
                user.Gender = (byte)userViewModel.Gender;

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
                    AddModelError(result);

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
                AppUser user = CurrentUser;

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
                            AddModelError(result);
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
