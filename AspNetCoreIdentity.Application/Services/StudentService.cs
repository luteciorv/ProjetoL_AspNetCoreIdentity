using AspNetCoreIdentity.Domain.Entities;
using AspNetCoreIdentity.Domain.Interfaces.Repositories;
using AspNetCoreIdentity.Domain.Interfaces.Services;

namespace AspNetCoreIdentity.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Student> GetAll() => _unitOfWork.StudentRepository.GetAll();
    }
}
