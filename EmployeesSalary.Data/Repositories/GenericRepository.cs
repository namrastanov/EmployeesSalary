using EmployeesSalary.Data.Entities.IEntities;
using EmployeesSalary.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeesSalary.Data.Repositories
{
    public class GenericRepository<TEntity, TId>: IGenericRepository<TEntity, TId>
        where TId : struct, IEquatable<TId>
        where TEntity : class, IEntity<TId>
    {
        internal DbContext _context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public virtual IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes
                    .Aggregate(query, (current, prop) => current.Include(prop));
            }

            return query;
        }

        public IQueryable<TResult> QuerySelect<TResult>(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, TResult>> selector =  null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Query(filter, includes);

            return query.Select(selector);
        }

        public async virtual Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public async virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            return (await dbSet.AddAsync(entity)).Entity;
        }

        public async virtual Task DeleteAsync(TId id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void ApplyCurrentValues(TEntity item, params Expression<Func<TEntity, object>>[] properties)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var entry = _context.Entry(item);
            if (properties.Any())
            {
                foreach (var property in properties)
                {
                    entry.Property(property).IsModified = true;
                }
            }
            else
            {
                if (entry.State == EntityState.Detached)
                {
                    var attacheItem = GetById(item.Id);
                    if (attacheItem != null)
                    {
                        var attachedEntry = _context.Entry(attacheItem);
                        attachedEntry.CurrentValues.SetValues(item);
                    }
                    else
                    {
                        entry.State = EntityState.Modified;
                    }
                }
            }
        }
    }
}
