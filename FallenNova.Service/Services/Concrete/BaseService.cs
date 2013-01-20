using FallenNova.DomainModel;
using System;

namespace FallenNova.Service
{
    public class BaseService : IDisposable
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public void Dispose()
        {
        }
    }
}
