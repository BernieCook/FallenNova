using FallenNova.DomainModel;
using FallenNova.Repository;
using Ninject.Modules;

namespace FallenNova.Service
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICharacterRepository>().To<CharacterRepository>();
            Bind<IEmail>().To<Email>();
            Bind<IEveOnlineConstellationRepository>().To<EveOnlineConstellationRepository>();
            Bind<IEveOnlineRegionRepository>().To<EveOnlineRegionRepository>();
            Bind<IEveOnlineRequiredSkillRepository>().To<EveOnlineRequiredSkillRepository>();
            Bind<IGenericRepository<EveOnlineSkill>>().To<GenericRepository<EveOnlineSkill>>();
            Bind<IGenericRepository<EveOnlineSkillGroup>>().To<GenericRepository<EveOnlineSkillGroup>>();
            Bind<IGenericRepository<EveOnlineSkillTree>>().To<GenericRepository<EveOnlineSkillTree>>();
            Bind<IEveOnlineSolarSystemRepository>().To<EveOnlineSolarSystemRepository>();
            Bind<IEveOnlineTypeRepository>().To<EveOnlineTypeRepository>();
            Bind<IGenericRepository<ContactUsLog>>().To<GenericRepository<ContactUsLog>>();
            Bind<IGenericRepository<EveOnlineAttribute>>().To<GenericRepository<EveOnlineAttribute>>();
            Bind<IGenericRepository<UserLog>>().To<GenericRepository<UserLog>>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserRoleRepository>().To<UserRoleRepository>();
        }
    }
}
