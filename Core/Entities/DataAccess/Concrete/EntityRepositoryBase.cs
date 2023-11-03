using Core.Entities.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities.DataAccess.Concrete
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity, TContext>
        where TEntity : class, IEntity, new()
        where TContext : IdentityDbContext<User>
    {
        private readonly TContext _dbContext;

        public EntityRepositoryBase(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Count()
        {
            return _dbContext.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> exp = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (exp != null)
            {
                query = query.Where(exp);
            }

            return await query.CountAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            return exp is null ? await query.ToListAsync() : await query.Where(exp).ToListAsync();
        }

        public IEnumerable<TEntity> GetAllIncluding(Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }
        public async Task<TEntity> GetIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            return query.Where(exp).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetEntityByIdsAsync(int[] ids)
        {
            return await _dbContext.Set<TEntity>().Where(c => ids.Contains(c.Id)).ToListAsync();
        }


        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp)
        {
            return await _dbContext.Set<TEntity>().AnyAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public List<TEntity> Skip(int count)
        {
            return _dbContext.Set<TEntity>().Skip(count).ToList();
        }

        public List<TEntity> Take(int count)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().Take(count);
            return query.Take(count).ToList();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
        private IQueryable<TEntity> GetQuery(string[] includes)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (includes is not null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }

    }
}
