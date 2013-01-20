using FallenNova.DomainModel;

namespace FallenNova.Repository
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        void DeleteByUserId(int userId);
    }
}
