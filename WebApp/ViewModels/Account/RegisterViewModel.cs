using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "Endereço de e-mail")]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de e-mail.")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Confirmar senha")]
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "As senhas digitadas são diferentes.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
