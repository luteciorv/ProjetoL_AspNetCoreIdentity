using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminClaimsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminClaimsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index() => View(_userManager.Users);

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user is null)
            {
                ModelState.AddModelError("", $"O usuário de id {id} não foi encontrado.");
                return View();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Claims = userClaims.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Id);
            if (user is null)
            {
                ModelState.AddModelError("", $"O usuário de id {model.Id} não foi encontrado.");
                return View();
            }

            user.Email = model.Email;
            user.UserName = model.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) 
            {
                foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Post(string claimType, string claimValue, string userId)
        {
            if(claimType is null || claimValue is null)
            {
                ModelState.AddModelError(string.Empty, "Tipo ou valor da claim não pode ser nulo.");
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                ModelState.AddModelError(string.Empty, $"O usuário de id {userId} não foi encontrado.");
                return View();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var claim = userClaims.FirstOrDefault(c => c.Type.Equals(claimType) && c.Value.Equals(claimValue));
            if(claim is not null)
            {
                ModelState.AddModelError(string.Empty, $"A reinvidicação do tipo {claim.Type} e valor {claim.Value} já existe.");
                return View();
            }

            var newClaim = new Claim(claimType, claimValue);
            var result = await _userManager.AddClaimAsync(user, newClaim);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string claimValues)
        {
            var claimValuesArray = claimValues.Split(';');
            string claimType = claimValuesArray[0];
            string claimValue = claimValuesArray[1];
            string userId = claimValuesArray[2];

            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                ModelState.AddModelError(string.Empty, $"O usuário de id {userId} não foi encontrado.");
                return View();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var claim = userClaims.FirstOrDefault(c => c.Type.Equals(claimType) && c.Value.Equals(claimValue));
            var result = await _userManager.RemoveClaimAsync(user, claim);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
