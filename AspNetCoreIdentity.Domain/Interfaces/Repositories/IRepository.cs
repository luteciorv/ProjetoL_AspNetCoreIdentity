namespace AspNetCoreIdentity.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(Guid id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
