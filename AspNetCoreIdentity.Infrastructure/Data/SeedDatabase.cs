using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using AspNetCoreIdentity.Infrastructure.Interfaces;
using System.Data;
using Microsoft.Extensions.Logging;

namespace AspNetCoreIdentity.Infrastructure.Data
{
    public class SeedDatabase : ISeedDatabase
    {
        private readonly List<string> _roles;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SeedDatabase> _logger;

        public SeedDatabase(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<SeedDatabase> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;

            _roles = new List<string>() { "User", "Manager", "Admin" };
        }

        public async Task SeedRolesAsync()
        {
            foreach (var role in _roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    var result = await _roleManager.CreateAsync(newRole);
                    if (!result.Succeeded)
                        _logger.LogError($"Não foi possível criar a role de nome {newRole.Name}.");
                }
            }
        }

        public async Task SeedUsersRolesAsync()
        {
            foreach (var role in _roles)
            {
                string email = $"{role.ToLower()}@localhost";
                if (await _userManager.FindByEmailAsync(email) is null)
                {
                    var newUser = new IdentityUser
                    {
                        Email = email,
                        NormalizedEmail = email.ToUpper(),
                        UserName = email,
                        NormalizedUserName = email.ToUpper(),
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await _userManager.CreateAsync(newUser, "Teste@123");
                    if (!result.Succeeded)
                        _logger.LogError($"Não foi possível criar o usuário de e-mail {newUser.Email}.");

                    await _userManager.AddToRoleAsync(newUser, role);
                }
            }
        }

        public async Task SeedUsersClaimsAsync()
        {
            // Administrador
            string email = "admin@localhost";
            var userAdmin = await _userManager.FindByEmailAsync(email);
            if(userAdmin is not null)
            {
                var claims = (await _userManager.GetClaimsAsync(userAdmin)).Select(c => c.Type);

                string claimName = "CadastradoEm";
                if (!claims.Contains(claimName))
                {
                    var newClaim = new Claim(claimName, "09/04/2014");
                    var result = await _userManager.AddClaimAsync(userAdmin, newClaim);
                    if (!result.Succeeded)
                        throw new Exception($"Não foi possível adicionar a Claim com o nome {claimName} ao usuário de e-mail {userAdmin.Email}.");
                }

                claimName = "IsAdmin";
                if (!claims.Contains(claimName))
                {
                    var newClaim = new Claim(claimName, "true");
                    var result = await _userManager.AddClaimAsync(userAdmin, newClaim);
                    if (!result.Succeeded)
                        throw new Exception($"Não foi possível adicionar a Claim com o nome {claimName} ao usuário de e-mail {userAdmin.Email}.");
                }
            }


            // Usuário
            email = "user@localhost";
            var user = await _userManager.FindByEmailAsync("user@localhost");
            if(user is not null)
            {
                var claims = (await _userManager.GetClaimsAsync(user)).Select(c => c.Type);

                string claimName = "CadastradoEm";
                if (!claims.Contains(claimName))
                {
                    var newClaim = new Claim(claimName, "01/01/2020");
                    var result = await _userManager.AddClaimAsync(user, newClaim);
                    if (!result.Succeeded)
                        throw new Exception($"Não foi possível adicionar a Claim com o nome {claimName} ao usuário de e-mail {user.Email}.");
                }

                claimName = "IsAdmin";
                if (!claims.Contains(claimName))
                {
                    var newClaim = new Claim(claimName, "false");
                    var result = await _userManager.AddClaimAsync(user, newClaim);
                    if (!result.Succeeded)
                        throw new Exception($"Não foi possível adicionar a Claim com o nome {claimName} ao usuário de e-mail {user.Email}.");
                }

                claimName = "IsEmployee";
                if (!claims.Contains(claimName))
                {
                    var newClaim = new Claim(claimName, "true");
                    var result = await _userManager.AddClaimAsync(user, newClaim);
                    if (!result.Succeeded)
                        throw new Exception($"Não foi possível adicionar a Claim com o nome {claimName} ao usuário de e-mail {user.Email}.");
                }
            }

            // Gerente
            var userManager = await _userManager.FindByEmailAsync("manager@localhost");
            if(userManager is not null)
            {
                var claims = (await _userManager.GetClaimsAsync(userManager)).Select(c => c.Type);

                string claimName = "CadastradoEm";
                if (!claims.Contains(claimName))
                {
                    var newClaim = new Claim(claimName, "03/03/2021");
                    var result = await _userManager.AddClaimAsync(userManager, newClaim);
                    if (!result.Succeeded)
                        throw new Exception($"Não foi possível adicionar a Claim com o nome {claimName} ao usuário de e-mail {userManager.Email}.");
                }

                claimName = "IsEmployee";
                if (!claims.Contains(claimName))
                {
                    var newClaim = new Claim(claimName, "true");
                    var result = await _userManager.AddClaimAsync(userManager, newClaim);
                    if (!result.Succeeded)
                        throw new Exception($"Não foi possível adicionar a Claim com o nome {claimName} ao usuário de e-mail {userManager.Email}.");
                }
            } 
        }
    }
}
