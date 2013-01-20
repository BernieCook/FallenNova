using FallenNova.DomainModel;

namespace FallenNova.Repository
{
    public interface IEveOnlineSolarSystemRepository : IGenericRepository<EveOnlineSolarSystem>, IEveOnlineRepository<EveOnlineSolarSystem>
    {
    }
}
