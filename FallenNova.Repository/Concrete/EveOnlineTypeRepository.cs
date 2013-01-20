using FallenNova.DomainModel;
using System.Linq;

namespace FallenNova.Repository
{
    public class EveOnlineTypeRepository : GenericRepository<EveOnlineType>, IEveOnlineTypeRepository
    {
        public EveOnlineTypeRepository(FallenNovaContext context)
            : base(context)
        {
        }

        public IQueryable<EveOnlineType> GetByName(
            string name)
        {
            return (string.IsNullOrWhiteSpace(name))
                ? GetAll()
                : GetAll(q => q.Name.Contains(name));
        }
    }
}