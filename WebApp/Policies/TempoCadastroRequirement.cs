using Microsoft.AspNetCore.Authorization;

namespace WebApp.Policies
{
    public class TempoCadastroRequirement : IAuthorizationRequirement
    {
        public TempoCadastroRequirement(int tempoMinimoCadastro)
        {
            TempoMinimoCadastro = tempoMinimoCadastro;
        }

        public int TempoMinimoCadastro { get; private set; }
    }
}
