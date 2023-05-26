using AspNetCoreIdentity.Application.DTOs.Student;
using AspNetCoreIdentity.Domain.Entities;

namespace AspNetCoreIdentity.Application.Interfaces.Services
{
    public interface IStudentService
    {
        IEnumerable<ReadStudentDto> GetAll();
        Task<ReadStudentDto?> GetByIdAsync(Guid id);
        Task<EditStudentDto> GetEditByIdAsync(Guid id);
        Task<DetailsStudentDto> GetDetailsByIdAsync(Guid id);
        Task<DeleteStudentDto> GetDeleteByIdAsync(Guid id);

        Task<ReadStudentDto> CreateAsync(CreateStudentDto studentDto);
        Task UpdateAsync(Guid id, EditStudentDto studentDto);
        Task DeleteById(Guid id);
    }
}
