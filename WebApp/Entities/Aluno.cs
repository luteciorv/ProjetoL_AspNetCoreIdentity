using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities
{
    public class Aluno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")] 
        [MaxLength(80, ErrorMessage = "Este campo não pode exceder 80 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Este campo precisa estar formatado como um endereço de e-mail")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(120, ErrorMessage = "Este campo não pode exceder 120 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, 150, ErrorMessage = "Informe um valor entre 1 e 150. As extremidades pode estar inclusas.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(80, ErrorMessage = "Este campo não pode exceder 80 caracteres.")]
        public string Curso { get; set; } = string.Empty;
    }
}
