using AspNetCoreIdentity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Domain.Interfaces.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _dataContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dataContext.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Added;
        }

        public void Delete(TEntity entity)
        {
            _dataContext.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Deleted;
        }

        public IEnumerable<TEntity> GetAll() => _dbSet.AsNoTracking().AsEnumerable();

        public async Task<TEntity?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public void Update(TEntity entity)
        {
            _dataContext.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
