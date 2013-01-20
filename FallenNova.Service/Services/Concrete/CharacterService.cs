using FallenNova.Repository;
using System.Collections.Generic;
using System.Linq;

namespace FallenNova.Service
{
    public class CharacterService : BaseService, ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterService(
            ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        #region Get Methods

        public IEnumerable<CharacterDetailsDto> GetCharacters(
            int pageIndex,
            int pageSize,
            string sortBy,
            bool sortAscending,
            out int totalUserCount)
        {
            return _characterRepository.GetAll(
                pageIndex,
                pageSize,
                sortBy,
                sortAscending,
                out totalUserCount).Select(c =>
                new CharacterDetailsDto
                {
                    CharacterId = c.CharacterId,
                    CharacterName = c.CharacterName,
                    CorporationName = c.Corporation.Name
                });
        }

        #endregion
    }
}
