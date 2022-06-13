﻿using CoreIdentityProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreIdentityProject.Controllers
{
    public class BaseController : Controller
    {
        protected UserManager<AppUser> _userManager { get; }
        protected SignInManager<AppUser> _signInManager { get; }
        protected RoleManager<AppRole> _roleManager { get; }

        protected AppUser CurrentUser => _userManager.FindByNameAsync(User.Identity.Name).Result;


        public BaseController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager=null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
