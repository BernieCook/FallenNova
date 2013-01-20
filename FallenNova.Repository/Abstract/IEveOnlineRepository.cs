using System.Linq;

namespace FallenNova.Repository
{
    public interface IEveOnlineRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetByName(string name);
    }
}
