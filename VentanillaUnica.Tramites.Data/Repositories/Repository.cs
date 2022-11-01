using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VentanillaUnica.Tramites.Domain.Entities;
using VentanillaUnica.Tramites.Domain.Repositories;

namespace VentanillaUnica.Tramites.Data.Repositories
{
    public class Repository<TId, TEntity> : IRepository<TId, TEntity>
        where TId : struct
        where TEntity : EntityBase<TId>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public TEntity FindById(dynamic id)
        {
            return _unitOfWork.GetSet<TId, TEntity>().Find(id);
        }

        public async Task<TEntity> FindByIdAsync(dynamic id)
        {
            return await _unitOfWork.GetSet<TId, TEntity>().FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            return BuildQuery(filter, orderBy, includeProperties).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            return await BuildQuery(filter, orderBy, includeProperties).ToListAsync();
        }

        public IEnumerable<TEntity> GetAllPaged(
            int take,
            int skip,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            return BuildQuery(filter, orderBy, includeProperties).Skip(skip).Take(take).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllPagedAsync(
            int take,
            int skip,
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            return await BuildQuery(filter, orderBy, includeProperties).Skip(skip).Take(take).ToListAsync();
        }

        public int Count(Expression<Func<TEntity, bool>>? filter = null)
        {
            return BuildQuery(filter).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            return await BuildQuery(filter).CountAsync();
        }

        public bool Any(Expression<Func<TEntity, bool>>? filter = null)
        {
            var item = _unitOfWork.GetSet<TId, TEntity>().AsNoTracking();
            return filter == null ? item.Any() : item.Any(filter);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            var item = _unitOfWork.GetSet<TId, TEntity>().AsNoTracking();
            return filter == null ? await item.AnyAsync() : await item.AnyAsync(filter);
        }

        public void Add(TEntity entity)
        {
            ValidateEntity(entity);
            _unitOfWork.GetSet<TId, TEntity>().Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            ValidateEntity(entity);
            await _unitOfWork.GetSet<TId, TEntity>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            ValidateRangeEntities(entities);
            _unitOfWork.GetSet<TId, TEntity>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            ValidateRangeEntities(entities);
            await _unitOfWork.GetSet<TId, TEntity>().AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            ValidateEntity(entity);
            _unitOfWork.GetSet<TId, TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            ValidateRangeEntities(entities);
            _unitOfWork.GetSet<TId, TEntity>().UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            ValidateEntity(entity);
            _unitOfWork.GetSet<TId, TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            ValidateRangeEntities(entities);
            _unitOfWork.GetSet<TId, TEntity>().RemoveRange(entities);
        }

        #region PrivateMethods
        private IQueryable<TEntity> BuildQuery(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            var query = _unitOfWork.GetSet<TId, TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(property =>
            {
                query = query.Include(property);
            });

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        private static void ValidateEntity(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "El objeto entidad no puede ser nulo");
            }
        }

        private static void ValidateRangeEntities(IEnumerable<TEntity> entities)
        {
            if (!entities?.Any() ?? true)
            {
                throw new ArgumentNullException(nameof(entities), "no se envió una lista de entidades a insertar");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
        }

        #endregion

    }
}
