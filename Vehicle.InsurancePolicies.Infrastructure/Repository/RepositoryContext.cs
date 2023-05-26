using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Vehicle.InsurancePolicies.Contracts.Repository;

namespace Vehicle.InsurancePolicies.Infrastructure.Repository
{
  public class RepositoryContext<TContext> : IRepositoryContext<TContext> where TContext : DbContext
  {
    readonly TContext _context;
    bool _disposed = false;

    public RepositoryContext(TContext context) => _context = context;

    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
    {
      return _context.Entry(entity);
    }

    public DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
      return _context.Set<TEntity>();
    }

    public int Save() => _context.SaveChanges();

    public Task<int> SaveAsync() => _context.SaveChangesAsync();

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
        return;
      if (disposing)
        _context.Dispose();
      _disposed = true;
    }
  }
}
