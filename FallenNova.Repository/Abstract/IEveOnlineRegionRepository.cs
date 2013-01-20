using FallenNova.DomainModel;

namespace FallenNova.Repository
{
    public interface IEveOnlineRegionRepository : IGenericRepository<EveOnlineRegion>, IEveOnlineRepository<EveOnlineRegion>
    {
    }
}
