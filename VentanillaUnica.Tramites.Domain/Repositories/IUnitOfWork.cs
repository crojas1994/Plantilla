using Microsoft.EntityFrameworkCore;
using VentanillaUnica.Tramites.Domain.Entities;

namespace VentanillaUnica.Tramites.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        DbContext GetContext();
        DbSet<TEntity> GetSet<TId, TEntity>()
            where TId : struct
            where TEntity : EntityBase<TId>;
    }
}
