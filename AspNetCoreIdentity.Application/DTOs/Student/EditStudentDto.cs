using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentity.Application.DTOs.Student
{
    public class EditStudentDto
    {
        [Required(ErrorMessage = "O campo 'Id' é orbrigatório.")]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Endereço de e-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um endereço de e-mail válido.")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Idade")]
        [Required(ErrorMessage =  "Este campo é obrigatório")]
        public int Age { get; set; }

        [Display(Name = "Curso")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Course { get; set; } = string.Empty;
    }
}