using FallenNova.DomainModel;
using System.Linq;

namespace FallenNova.Repository
{
    public class EveOnlineConstellationRepository : GenericRepository<EveOnlineConstellation>, IEveOnlineConstellationRepository
    {
        public EveOnlineConstellationRepository(FallenNovaContext context) 
            : base(context)
        {
        }

        public IQueryable<EveOnlineConstellation> GetByName(
            string name)
        {
            return (string.IsNullOrWhiteSpace(name))
                ? GetAll()
                : GetAll(q => q.Name.Contains(name));
        }
    }
}