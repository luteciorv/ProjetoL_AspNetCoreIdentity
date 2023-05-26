using AspNetCoreIdentity.Domain.Entities;

namespace AspNetCoreIdentity.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Student> StudentRepository { get; }
        IRepository<Product> ProductRepository { get; }

        Task CommitAsync();
    }
}
