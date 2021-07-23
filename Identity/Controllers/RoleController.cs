using Identity.Models.Authentication;
using Identity.Models.Claims;
using Identity.Models.DbContexts;
using Identity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());

        }

        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {

            IdentityResult result = null;

            result = await _roleManager.CreateAsync(new AppRole { Name = model.Name, RecordDate = DateTime.Now });

            if (result.Succeeded)
            {
                //Başarılı...
            }
            return RedirectToAction("Index", "Role");
        }
        public async Task<IActionResult> DeleteRole(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                //Başarılı...
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RoleAssign(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            List<AppRole> allRoles = _roleManager.Roles.ToList();
            List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            List<RoleAssignViewModel> assignRoles = new List<RoleAssignViewModel>();
            allRoles.ForEach(role => assignRoles.Add(new RoleAssignViewModel
            {
                HasAssign = userRoles.Contains(role.Name),
                RoleId = role.Id,
                RoleName = role.Name
            }));

            return View(assignRoles);
        }
        [HttpPost]
        public async Task<ActionResult> RoleAssign(List<RoleAssignViewModel> modelList, string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            foreach (RoleAssignViewModel role in modelList)
            {
                if (role.HasAssign)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                else
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
            }
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> AssignAuthority(Guid Id)
        {
            List<AssignAuthorityViewModel> model = new List<AssignAuthorityViewModel>();
            var role = await _roleManager.Roles.Where(t => t.Id == Id).FirstOrDefaultAsync();

            var roleClaims = await _context.RoleClaims.Where(t => t.RoleId == role.Id).Select(t => t.ClaimType).ToListAsync();
            
            var allClaims = ClaimData.AllClaims.ToList();
            allClaims.ForEach(claim => model.Add(new AssignAuthorityViewModel
            {
                HasAssign = roleClaims.Contains(claim.Type),
                Id = role.Id,
                Type = claim.Type,
                Value = claim.Value,
            }));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignAuthority(List<AssignAuthorityViewModel> model, Guid Id)
        {
            var role = await _roleManager.Roles.Where(t => t.Id == Id).FirstOrDefaultAsync();

            var roleClaims = await _context.RoleClaims.ToListAsync();
            if (model != null && model.Count > 0)
            {
                foreach (var item in roleClaims)
                {

                    _context.RoleClaims.Remove(item);

                }
               await _context.SaveChangesAsync();
                foreach (var item in model)
                {
                    if (item.HasAssign)
                    {
                        await _roleManager.AddClaimAsync(role, new Claim(item.Type, item.Value));

                    }
                }

            }



            return RedirectToAction("Index", "Role");
        }
    }
}
