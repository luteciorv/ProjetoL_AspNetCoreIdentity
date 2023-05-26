using AspNetCoreIdentity.Application.DTOs.Student;
using AspNetCoreIdentity.Domain.Entities;
using AspNetCoreIdentity.Domain.Interfaces.Repositories;
using AspNetCoreIdentity.Application.Interfaces.Services;
using AutoMapper;

namespace AspNetCoreIdentity.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateStudentDto studentDto)
        {          
            var student = _mapper.Map<Student>(studentDto);
            _unitOfWork.StudentRepository.Add(student);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteById(Guid id)
        {
            var student = await GetByIdAsync(id);
            if (student is null)
                return;

            _unitOfWork.StudentRepository.Delete(student);
            await _unitOfWork.CommitAsync();
        }

        public IEnumerable<ReadStudentDto> GetAll()
        {
            var students = _unitOfWork.StudentRepository.GetAll();
            return _mapper.Map<IEnumerable<ReadStudentDto>>(students);
        }

        public async Task<Student?> GetByIdAsync(Guid id) =>
            await _unitOfWork.StudentRepository.GetByIdAsync(id);

        public async Task<DetailsStudentDto> GetDetailsByIdAsync(Guid id)
        {
            var student = await GetByIdAsync(id);
            return _mapper.Map<DetailsStudentDto>(student);
        }

        public async Task<EditStudentDto> GetEditByIdAsync(Guid id)
        {
            var student = await GetByIdAsync(id);
            return _mapper.Map<EditStudentDto>(student);
        }

        public async Task<DeleteStudentDto> GetDeleteByIdAsync(Guid id)
        {
            var student = await GetByIdAsync(id);
            return _mapper.Map<DeleteStudentDto>(student);
        }

        public async Task UpdateAsync(EditStudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            _unitOfWork.StudentRepository.Update(student);
            await _unitOfWork.CommitAsync();
        }
    }
}
