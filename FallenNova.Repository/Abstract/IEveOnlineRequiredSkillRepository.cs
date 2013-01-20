using FallenNova.DomainModel;

namespace FallenNova.Repository
{
    public interface IEveOnlineRequiredSkillRepository : IGenericRepository<EveOnlineRequiredSkill>
    {
        void DeleteAll();
    }
}
