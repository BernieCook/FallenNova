using FallenNova.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FallenNova.Repository
{
    /// <summary>
    /// Generic repository class.
    /// </summary>
    /// <typeparam name="TEntity">Entity.</typeparam>
    /// <remarks>
    /// I didn't opt for the light-weight generic repository approach because I wanted to reduce the amount of LINQ I had to write in 
    /// the service layer. Both approaches have their advantages and disadvantages. 
    /// Tip: Avoid creating repositories for non-root aggregate entities - DDD best practice.
    /// </remarks>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public readonly FallenNovaContext Context;
        public readonly DbSet<TEntity> DbSet;

        #region Constructor
        
        public GenericRepository(FallenNovaContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        #endregion

        #region Get Methods

        public TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public TEntity Get(
            Expression<Func<TEntity, bool>> filter = null,
            IEnumerable<string> includePaths = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includePaths != null)
            {
                query = includePaths.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query.SingleOrDefault();
        }

        public TEntity Get(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var result = GetAll();

            if (includes.Any())
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }

            return result.FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            int takeCount,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return await orderBy(DbSet).Take(takeCount).ToListAsync();
        }

        public IEnumerable<TEntity> GetAll(
            out int totalCount,
            int skipCount,
            int takeCount,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<string> includePaths = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includePaths != null)
            {
                query = includePaths.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            // NOTE: The following logic retrieves the count and returns the entities using one query.
            IEnumerable<TEntity> entities = (orderBy != null)
                ? orderBy(query).ToList()
                : query.ToList();

            totalCount = entities.Count();

            return entities.Skip(skipCount).Take(takeCount);
        }

        public IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var result = GetAll();

            if (includes.Any())
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }

            return result.Where(predicate);
        }

        public IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            IEnumerable<string> includePaths = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includePaths != null)
            {
                query = includePaths.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = DbSet;

            return query;
        }

        #endregion

        #region Insert Methods

        public void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        #endregion

        #region Update Methods

        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);

            Context.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region Delete Methods

        public void Delete(object id)
        {
            var entity = DbSet.Find(id);

            Delete(entity);
        }

        public void Delete(IQueryable query)
        {
            foreach (TEntity entity in query)
            {
                Delete(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
     
            DbSet.Remove(entity);
        }

        #endregion
    }
}