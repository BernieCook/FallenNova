using FallenNova.Service;

namespace FallenNova.Web
{
    public interface IBaseController
    {
        CustomClaimsIdentity CurrentUser { get; }
    }
}