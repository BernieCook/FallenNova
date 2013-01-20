using FallenNova.DomainModel;

namespace FallenNova.Repository
{
    // NOTE: The user/user role/role architecture is designed to support a one (user) to many (roles) relationship.
    // At the time of writing this particular solution only requires a single one (user) to one (role) relationship. 

    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(FallenNovaContext context) 
            : base(context)
        {
        }

        public void DeleteByUserId(int userId)
        {
            foreach (var userRole in GetAll(ur => ur.UserId == userId))
            {
                Delete(userRole);
            }
        }
    }
}