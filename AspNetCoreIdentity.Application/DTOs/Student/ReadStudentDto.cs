using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentity.Application.DTOs.Student
{
    public class ReadStudentDto
    {
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Endereço de e-mail")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Idade")]
        public int Age { get; set; }

        [Display(Name = "Curso")]
        public string Course { get; set; } = string.Empty;
    }
}
