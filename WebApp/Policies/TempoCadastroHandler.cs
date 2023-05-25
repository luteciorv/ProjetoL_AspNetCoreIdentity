using Microsoft.AspNetCore.Authorization;

namespace WebApp.Policies
{
    public class TempoCadastroHandler : AuthorizationHandler<TempoCadastroRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TempoCadastroRequirement requirement)
        {
            if(context.User.HasClaim(c => c.Type == "CadastradoEm"))
            {
                var claimData = context.User.FindFirst(c => c.Type == "CadastradoEm");
                if (claimData is null)
                    return;

                DateTime dataCadastro = DateTime.Parse(claimData.Value);
                double tempoCadastro = await Task.Run(() => (DateTime.Now.Date -  dataCadastro.Date).TotalDays);
                double tempoEmAnos = tempoCadastro / 360d;

                if (tempoEmAnos >= requirement.TempoMinimoCadastro)
                    context.Succeed(requirement);
            }
        }
    }
}
