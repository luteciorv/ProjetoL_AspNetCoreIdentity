using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Endereço de e-mail")]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de e-mail.")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Lembrar do acesso?")]
        public bool RememberMe { get; set; }
    }
}
