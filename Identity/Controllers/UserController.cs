using Identity.Models.Authentication;
using Identity.Models.DbContexts;
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
        private readonly AppDbContext _context;
        public UserController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }
        public IActionResult Index()
        {
            var userList = _context.Users.ToList();
            return View(userList);
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
                else
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            }
            return View();
        }
    }
}
