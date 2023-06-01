using System.Linq.Expressions;
using MongoFramework;

namespace Vehicle.InsurancePolicies.Contracts.Mongo
{
  public interface IRepository<in TContext, TEntity>
    where TContext : MongoDbContext
    where TEntity : class
  {
    void Create(TEntity entity);
    void CreateRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    TEntity? Find(object id);
    TEntity? Find(Expression<Func<TEntity, bool>> predicate);
    bool Exists(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> Get(params Expression<Func<TEntity, bool>>[] includes);
    IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);
    IEnumerable<TEntity> GetByOrder(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
  }
}
