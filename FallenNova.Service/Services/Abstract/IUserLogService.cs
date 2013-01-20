using System;

namespace FallenNova.Service
{
    public interface IUserLogService : IDisposable
    {
        void AuthenticatedSuccessfully(int userId);
        void LoginSuccessful(int userId);
        void LoginUnsuccessful(string emailAddress);
        void LoggedOut(int userId);
    }
}
