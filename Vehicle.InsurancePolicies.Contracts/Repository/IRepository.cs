using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Vehicle.InsurancePolicies.Contracts.Repository
{
  public interface IRepository<in TContext, TEntity>
    where TContext : DbContext
    where TEntity : class
  {
    TEntity Create(TEntity entity);
    IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities);
    TEntity Update(TEntity entity);
    IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
    TEntity Delete(TEntity entity);
    IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entities);
    TEntity? Find(params object[] primaryKeys);
    TEntity? Find(Expression<Func<TEntity, bool>> predicate);
    bool Exists(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> Get(params Expression<Func<TEntity, bool>>[] includes);
    IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);
    IEnumerable<TEntity> GetByOrder(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
  }
}
