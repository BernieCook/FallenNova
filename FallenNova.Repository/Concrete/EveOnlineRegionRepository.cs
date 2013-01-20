using FallenNova.DomainModel;
using System.Linq;

namespace FallenNova.Repository
{
    public class EveOnlineRegionRepository : GenericRepository<EveOnlineRegion>, IEveOnlineRegionRepository
    {
        public EveOnlineRegionRepository(FallenNovaContext context) 
            : base(context)
        {
        }

        public IQueryable<EveOnlineRegion> GetByName(
            string name)
        {
            return (string.IsNullOrWhiteSpace(name))
                ? GetAll()
                : GetAll(q => q.Name.Contains(name));
        }
    }
}