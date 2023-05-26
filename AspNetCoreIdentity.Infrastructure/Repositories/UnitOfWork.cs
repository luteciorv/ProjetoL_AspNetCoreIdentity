using AspNetCoreIdentity.Domain.Interfaces.Repositories;
using AspNetCoreIdentity.Infrastructure.Data;
using AspNetCoreIdentity.Domain.Entities;

namespace AspNetCoreIdentity.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _dataContext;

        private Repository<Student> _studentRepository;
        public IRepository<Student> StudentRepository
        {
            get
            {
                _studentRepository ??= new Repository<Student>(_dataContext);
                return _studentRepository;
            }
        }

        private Repository<Product> _productRepository;
        public IRepository<Product> ProductRepository
        {
            get
            {
                _productRepository ??= new Repository<Product>(_dataContext);
                return _productRepository;
            }
        }

        public UnitOfWork(DataContext dataContext) => _dataContext = dataContext;

        public void Commit() => _dataContext.SaveChanges();

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if(_disposed)
                if(disposing)
                    _dataContext.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
