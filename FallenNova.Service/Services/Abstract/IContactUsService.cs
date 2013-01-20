using System;
using System.Collections.Generic;

namespace FallenNova.Service
{
    public interface IContactUsService : IDisposable
    {
        bool Insert(ContactUsDetailsDto contactUsDetailsDto, int? addedByUserId, ref IList<string> errorMessages);
    }
}
