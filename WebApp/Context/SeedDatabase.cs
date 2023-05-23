using Microsoft.AspNetCore.Identity;
using WebApp.Interfaces;

namespace WebApp.Context
{
    public class SeedDatabase : ISeedDatabase
    {
        private readonly List<string> _roles;
        private readonly List<string> _users;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedDatabase(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            _roles = new List<string>() { "User", "Manager", "Admin" };
            _users = new List<string>() { "teste_02@gmail.com:User", "teste_03@gmail.com:User", "teste_04@gmail.com:Manager", "teste_05@gmail.com:Admin" };
        }

        public async Task SeedRolesAsync()
        {
            foreach(var role in _roles)
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
                        throw new Exception($"Não foi possível criar a role de nome {newRole.Name}.");
                }
            }        
        }

        public async Task SeedUsersAsync()
        {
            foreach(var user in _users)
            {
                string email = user.Split(':')[0];
                string role = user.Split(':')[1];

                if(await _userManager.FindByEmailAsync(email) is null)
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
                        throw new Exception($"Não foi possível criar o usuário de e-mail {newUser.Email}.");
                  
                    await _userManager.AddToRoleAsync(newUser, role);
                }
            }
        }
    }
}
