using FallenNova.DomainModel;
using System.Linq;

namespace FallenNova.Repository
{
    public class EveOnlineSolarSystemRepository : GenericRepository<EveOnlineSolarSystem>, IEveOnlineSolarSystemRepository
    {
        public EveOnlineSolarSystemRepository(FallenNovaContext context) 
            : base(context)
        {
        }

        public IQueryable<EveOnlineSolarSystem> GetByName(
            string name)
        {
            return (string.IsNullOrWhiteSpace(name)) 
                ? GetAll() 
                : GetAll(q => q.Name.Contains(name));
        }
    }
}