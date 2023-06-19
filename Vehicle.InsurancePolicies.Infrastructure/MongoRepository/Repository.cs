using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MongoFramework;
using Vehicle.InsurancePolicies.Contracts.MongoRepository;

namespace Vehicle.InsurancePolicies.Infrastructure.MongoRepository
{
  public class Repository<TContext, TEntity> : IRepository<TContext, TEntity>
    where TContext : MongoDbContext
    where TEntity : class
  {
    readonly IRepositoryContext<TContext> _context;
    readonly IMongoDbSet<TEntity> _entitySet;
    readonly IQueryable<TEntity> _query;

    public Repository(IRepositoryContext<TContext> context)
    {
      _context = context;
      _entitySet = _context.Set<TEntity>();
      _query = _context.Query<TEntity>();
    }

    public void Create(TEntity entity) => _entitySet.Add(entity);

    public void CreateRange(IEnumerable<TEntity> entities) => _entitySet.AddRange(entities);

    public void Update(TEntity entity) => _entitySet.Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities) => _entitySet.UpdateRange(entities);

    public void Delete(TEntity entity) => _entitySet.Remove(entity);

    public void DeleteRange(IEnumerable<TEntity> entities) => _entitySet.RemoveRange(entities);

    public TEntity? Find(object id) => _entitySet.Find(id);

    public TEntity? Find(Expression<Func<TEntity, bool>> predicate) => _query.FirstOrDefault(predicate);

    public bool Exists(Expression<Func<TEntity, bool>> predicate) => _query.Any(predicate);

    public IEnumerable<TEntity> Get(params Expression<Func<TEntity, bool>>[] includes)
    {
      if (!includes.Any())
        return _query.ToList();
      var query = _query;
      foreach (var expression in includes)
        query = query.Include(expression);

      return query.ToList();
    }

    public IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter) => _query.Where(filter).ToList();

    public IEnumerable<TEntity> GetByOrder(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy) => orderBy(_query).ToList();
  }
}
