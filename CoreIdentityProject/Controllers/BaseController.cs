﻿using CoreIdentityProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentityProject.Controllers
{
    public class BaseController : Controller
    {
        protected UserManager<AppUser> _userManager { get; }
        protected SignInManager<AppUser> _signInManager { get; }

        protected AppUser CurrentUser => _userManager.FindByNameAsync(User.Identity.Name).Result;


        public BaseController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public void AddModelError(IdentityResult result)
        {

            foreach (IdentityError item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }


    }
}
