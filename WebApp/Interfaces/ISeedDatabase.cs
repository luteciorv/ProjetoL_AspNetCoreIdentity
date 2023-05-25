namespace WebApp.Interfaces
{
    public interface ISeedDatabase
    {
        Task SeedRolesAsync();
        Task SeedUsersAsync();

        Task SeedUsersClaimsAsync();
    }
}
