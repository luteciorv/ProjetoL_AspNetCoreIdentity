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

        public void Add(TEntity entity) => _dbSet.Add(entity);

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public IEnumerable<TEntity> GetAll() => _dbSet.AsEnumerable();

        public TEntity? GetById(string id) => _dbSet.Find(id);

        public void Update(TEntity entity) => _dbSet.Update(entity);  
    }
}
