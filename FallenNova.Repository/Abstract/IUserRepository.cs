using FallenNova.DomainModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FallenNova.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetAll(int skipCount, int takeCount, string sortBy, bool sortAscending, out int totalCount);
        Task<IEnumerable<User>> GetLatestAsync(int takeCount);
        IQueryable<User> GetByEmailAddress(string emailAddress);
    }
}
