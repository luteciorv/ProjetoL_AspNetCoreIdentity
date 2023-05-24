using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminRolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index() => View(_roleManager.Roles);


        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Required] string name)
        {
            if(!ModelState.IsValid)
                return View(name);

            var result = await _roleManager.CreateAsync(new IdentityRole(name));
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(name);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            var members = new List<IdentityUser>();
            var nonMembers = new List<IdentityUser>();

            foreach(var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }

            var roleEdit = new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };

            return View(roleEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RoleModification model)
        {
            if(!ModelState.IsValid)
                return await Update(model.RoleId);

            IdentityResult result;

            foreach(string userId in model.AddIds ?? Array.Empty<string>())
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                    continue;

                result = await _userManager.AddToRoleAsync(user, model.RoleName);
                if(!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            foreach(string userId in model.DeleteIds ?? Array.Empty<string>())
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                    continue;

                result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role is null)
            {
                ModelState.AddModelError(string.Empty, $"O perfil de id {id} não foi encontrado");
                return View(nameof(Index), _roleManager.Roles);
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                ModelState.AddModelError(string.Empty, $"O perfil de id {id} não foi encontrado");
                return View(nameof(Index), _roleManager.Roles);
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(nameof(Index), _roleManager.Roles);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
