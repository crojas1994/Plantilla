using System.Linq.Expressions;
using VentanillaUnica.Tramites.Domain.Entities;

namespace VentanillaUnica.Tramites.Domain.Repositories
{
    public interface IRepository<TId, TEntity> : IDisposable
        where TId : struct
        where TEntity : EntityBase<TId>
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        bool Any(Expression<Func<TEntity, bool>>? filter = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null);
        int Count(Expression<Func<TEntity, bool>>? filter = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        TEntity FindById(dynamic id);
        Task<TEntity> FindByIdAsync(dynamic id);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        IEnumerable<TEntity> GetAllPaged(int take, int skip, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAllPagedAsync(int take, int skip, Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "");
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}
