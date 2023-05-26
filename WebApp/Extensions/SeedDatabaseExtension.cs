using AspNetCoreIdentity.Infrastructure.Interfaces;

namespace WebApp.Extensions
{
    public static class SeedDatabaseExtension
    {
        public static async Task SeedDatabase(this WebApplication app)
        {
            var scopedFactory = 
                app.Services.GetService<IServiceScopeFactory>() ?? 
                throw new Exception($"Ocorreu um erro ao recuperar o serviço do {nameof(IServiceScopeFactory)}.");
            
            using var scope = scopedFactory.CreateScope();
           
            var service = 
                scope.ServiceProvider.GetService<ISeedDatabase>() ??
                throw new Exception($"Ocorreu um erro ao recuperar o serviço do {nameof(ISeedDatabase)}.");

            await service.SeedRolesAsync();
            await service.SeedUsersRolesAsync();
            await service.SeedUsersClaimsAsync();
        }
    }
}
