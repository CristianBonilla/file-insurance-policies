using MongoFramework;
using Vehicle.InsurancePolicies.Contracts.MongoRepository;

namespace Vehicle.InsurancePolicies.Infrastructure.MongoRepository
{
  public class RepositoryContext<TContext> : IRepositoryContext<TContext> where TContext : MongoDbContext
  {
    readonly TContext _context;
    bool _disposed = false;

    public RepositoryContext(TContext context) => _context = context;

    public void Attach<TEntity>(TEntity entity) where TEntity : class
    {
      _context.Attach(entity);
    }

    public void AttachRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
      _context.AttachRange(entities);
    }

    public MongoDbSet<TEntity> Set<TEntity>() where TEntity : class
    {
      return (MongoDbSet<TEntity>)_context.Set<TEntity>();
    }

    public IQueryable<TEntity> Query<TEntity>() where TEntity : class
    {
      return _context.Query<TEntity>();
    }

    public void Save() => _context.SaveChanges();

    public Task SaveAsync() => _context.SaveChangesAsync();

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
