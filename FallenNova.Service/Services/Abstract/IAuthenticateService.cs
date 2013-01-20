using System;

namespace FallenNova.Service
{
    public interface IAuthenticateService : IDisposable
    {
        bool ValidateUser(string emailAddress, string password);
        bool ValidateUser(int userId);

        void SetCurrentPrincipal(int userId);
    }
}
