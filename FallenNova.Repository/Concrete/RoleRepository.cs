using FallenNova.DomainModel;
using System.Linq;

namespace FallenNova.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(FallenNovaContext context) 
            : base(context)
        {
        }

        public IQueryable<Role> GetByUserId(int userId)
        {
            return from r in Context.Roles
                   join ur in Context.UserRoles on r.RoleId equals ur.RoleId
                   where (ur.UserId == userId)
                   select r;
        }
    }
}