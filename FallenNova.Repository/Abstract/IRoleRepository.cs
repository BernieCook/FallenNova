using FallenNova.DomainModel;
using System.Linq;

namespace FallenNova.Repository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        IQueryable<Role> GetByUserId(int userId);
    }
}
