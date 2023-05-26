using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentity.Application.DTOs.Student
{
    public class DeleteStudentDto
    {
        public Guid Id { get; private set; }

        [Display(Name = "Nome")]
        public string Name { get; private set; } = string.Empty;

        [Display(Name = "Endereço de e-mail")]
        public string Email { get; private set; } = string.Empty;
    }
}
