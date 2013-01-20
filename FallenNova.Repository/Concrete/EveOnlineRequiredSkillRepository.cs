using FallenNova.DomainModel;
using System.Linq;

namespace FallenNova.Repository
{
    public class EveOnlineRequiredSkillRepository : GenericRepository<EveOnlineRequiredSkill>, IEveOnlineRequiredSkillRepository 
    {
        public EveOnlineRequiredSkillRepository(FallenNovaContext context) 
            : base(context)
        {
        }

        public void DeleteAll()
        {
            // TODO: Implement a more efficient solution.
            Delete(
                from rs in Context.EveOnlineRequiredSkills select rs);
        }
    }
}