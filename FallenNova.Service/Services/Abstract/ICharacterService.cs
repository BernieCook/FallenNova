using System;
using System.Collections.Generic;

namespace FallenNova.Service
{
    public interface ICharacterService : IDisposable
    {
        IEnumerable<CharacterDetailsDto> GetCharacters(int pageIndex, int pageSize, string sortBy, bool sortAscending, out int totalUserCount);
    }
}
