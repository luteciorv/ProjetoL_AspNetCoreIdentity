using AspNetCoreIdentity.Application.DTOs.Student;
using AspNetCoreIdentity.Domain.Entities;
using AutoMapper;

namespace AspNetCoreIdentity.Application.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<CreateStudentDto, Student>();

            CreateMap<Student, EditStudentDto>().ReverseMap();
            CreateMap<Student, ReadStudentDto>();
            CreateMap<Student, DetailsStudentDto>();
            CreateMap<Student, DeleteStudentDto>();
        }
    }
}
