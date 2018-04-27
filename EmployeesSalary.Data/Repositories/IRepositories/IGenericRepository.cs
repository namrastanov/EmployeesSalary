using EmployeesSalary.Data.Entities.IEntities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Repositories.IRepositories
{
    public interface IGenericRepository<TEntity, TId>
        where TId : struct, IEquatable<TId>
        where TEntity : class, IEntity<TId>
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TResult> QuerySelect<TResult>(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, TResult>> selector = null,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetByIdAsync(object id);

        TEntity GetById(object id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task DeleteAsync(TId id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        void ApplyCurrentValues(TEntity item, params Expression<Func<TEntity, object>>[] properties);
    }
}
