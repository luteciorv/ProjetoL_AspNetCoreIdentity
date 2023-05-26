using AspNetCoreIdentity.Domain.Entities;

namespace AspNetCoreIdentity.Domain.Interfaces.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();
    }
}
