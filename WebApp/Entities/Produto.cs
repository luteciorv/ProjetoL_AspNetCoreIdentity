using System.ComponentModel.DataAnnotations;

namespace WebApp.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(80, ErrorMessage = "Este campo não pode exceder 80 caracteres")]
        public string Nome { get; set; } = string.Empty;

        public decimal Preco { get; set; }
    }
}
