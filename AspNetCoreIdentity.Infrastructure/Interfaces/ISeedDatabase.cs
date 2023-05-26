namespace AspNetCoreIdentity.Infrastructure.Interfaces
{
    public interface ISeedDatabase
    {
        Task SeedRolesAsync();
        Task SeedUsersRolesAsync();
        Task SeedUsersClaimsAsync();
    }
}
