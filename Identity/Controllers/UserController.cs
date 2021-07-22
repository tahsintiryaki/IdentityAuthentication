using Identity.Models.Authentication;
using Identity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = appUserViewModel.UserName,
                    Email = appUserViewModel.Email
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, appUserViewModel.Sifre);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}
