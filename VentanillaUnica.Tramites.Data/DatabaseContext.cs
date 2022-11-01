using Microsoft.EntityFrameworkCore;
using System.Transactions;
using VentanillaUnica.Tramites.Domain.IResources;
using VentanillaUnica.Tramites.Domain.Entities;
using VentanillaUnica.Tramites.Domain.Repositories;

namespace VentanillaUnica.Tramites.Data
{
    public class DatabaseContext : DbContext, IUnitOfWork
    {
        private readonly string _schema;

        public DatabaseContext(IConfigProvider configProvider, DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions)
        {
            _schema = configProvider.SchemaName;
        }

        public DbContext GetContext()
        {
            return this;
        }

        public DbSet<TEntity> GetSet<TId, TEntity>()
            where TId : struct
            where TEntity : EntityBase<TId>
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                return;
            }

            modelBuilder.HasDefaultSchema(_schema);
            modelBuilder.Entity<Parameter>().ToTable(nameof(Parameter)).HasKey(prop => prop.Id);

            base.OnModelCreating(modelBuilder);
        }

        public void Commit()
        {
            try
            {
                using var transactionScope = new TransactionScope();
                SaveChanges();
                transactionScope.Complete();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                await SaveChangesAsync();
                transactionScope.Complete();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }
    }
}
