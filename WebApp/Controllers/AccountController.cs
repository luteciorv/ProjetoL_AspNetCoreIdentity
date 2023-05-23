using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Account;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Mapear a ViewModel para o IdentityUser
            var user = new IdentityUser
            {   
                UserName = viewModel.Email,
                Email = viewModel.Email
            };

            // Criar usuário
            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(nameof(viewModel.ConfirmPassword), error.Description);

                return View(viewModel);
            }

            // Logar o usuário
            await _signInManager.SignInAsync(user, isPersistent: false);

            // Retornar para a tela inicial
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            // Validar modelo
            if (!ModelState.IsValid)
                return View(viewModel);

            // Logar o usuário
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false);
            if(!result.Succeeded)
            {
                ModelState.AddModelError(nameof(viewModel.Password), "Usuário ou senha inválido");
                return View(viewModel);
            }

            // Redirecionar para a página principal
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}
