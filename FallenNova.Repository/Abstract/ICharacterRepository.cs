using FallenNova.DomainModel;
using System.Collections.Generic;

namespace FallenNova.Repository
{
    public interface ICharacterRepository : IGenericRepository<Character>
    {
        IEnumerable<Character> GetAll(int pageIndex, int pageSize, string sortBy, bool sortAscending, out int totalUserCount);
    }
}
