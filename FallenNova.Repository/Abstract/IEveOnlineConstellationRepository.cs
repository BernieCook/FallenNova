using FallenNova.DomainModel;

namespace FallenNova.Repository
{
    public interface IEveOnlineConstellationRepository : IGenericRepository<EveOnlineConstellation>, IEveOnlineRepository<EveOnlineConstellation>
    {
    }
}
