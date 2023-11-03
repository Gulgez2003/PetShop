namespace Core.Entities.DataAccess.Abstract
{
    public interface IEntityRepository<TEntity, TContext>
        where TEntity : class, IEntity, new()
        where TContext : IdentityDbContext<User>
    {

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        IEnumerable<TEntity> GetAllIncluding(Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetEntityByIdsAsync(int[] Ids);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> exp = null);
        List<TEntity> Take(int count);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveAsync();
        int Count();
        List<TEntity> Skip(int count);
    }
}
