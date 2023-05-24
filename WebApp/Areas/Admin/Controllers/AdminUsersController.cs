using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index() => View(_userManager.Users);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user is null)
            {
                ViewBag.ErrorMessage = $"O usuário com o id {id} não foi encontrado";
                return View("NotFound");
            }

            var result = await _userManager.DeleteAsync(user);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
