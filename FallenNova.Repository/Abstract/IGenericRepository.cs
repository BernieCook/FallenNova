using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FallenNova.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAsync(int takeCount, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IEnumerable<TEntity> GetAll(out int totalCount, int skipCount, int takeCount, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, IEnumerable<string> includePaths = null);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, IEnumerable<string> includePaths = null);
        IQueryable<TEntity> GetAll();

        void Insert(TEntity entity);
        
        void Update(TEntity entity);
        
        void Delete(object id);
        void Delete(IQueryable query);
        void Delete(TEntity entity);
    }
}