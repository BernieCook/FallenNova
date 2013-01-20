using System;
using System.Collections.Generic;

namespace FallenNova.Service
{
    public interface IEveOnlineService : IDisposable
    {
        IEnumerable<ItemDatabaseDetailsDto> GetItems(int pageIndex, int pageSize, string sortBy, bool sortAscending, out int totalUserCount, string keywords, bool includeItems, bool includeGalacticObjects);

        ConstellationDetailsDto GetConstellationDetails(int id);
        RegionDetailsDto GetRegionDetails(int id);
        SolarSystemDetailsDto GetSolarSystemDetails(int id);

        ItemDetailsDto GetItemDetails(int id);
        SkillDetailsDto GetSkillDetails(int id);
        SkillGroupDetailsDto GetSkillGroupDetails(int id);

        IEnumerable<SkillGroupDto> GetSkillTree(out int totalSkillGroupCount);

        bool UpdateSkillTree(SkillTreeDetailsDto skillTreeDetailsDto, int addedByUserId, ref IList<string> errorMessages);
    }
}
