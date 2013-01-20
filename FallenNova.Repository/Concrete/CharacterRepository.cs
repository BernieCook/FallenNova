using FallenNova.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FallenNova.Repository
{
    public class CharacterRepository : GenericRepository<Character>, ICharacterRepository
    {
        private const string ConstCorporation = "corporation";

        public CharacterRepository(FallenNovaContext context)
            : base(context)
        {
        }

        public IEnumerable<Character> GetAll(
            int pageIndex,
            int takeCount,
            string sortBy,
            bool sortAscending, 
            out int totalCount)
        {
            // TODO: Correct the navigational property call below.
            return GetAll(
                out totalCount,
                ((pageIndex - 1) * takeCount),
                takeCount,
                u => u.CharacterName != null,
                GetOrderBy(sortBy, sortAscending),
                new List<string> { ConstCorporation });
        }

        private Func<IQueryable<Character>, IOrderedQueryable<Character>> GetOrderBy(
            string sortBy,
            bool sortAscending)
        {
            Func<IQueryable<Character>, IOrderedQueryable<Character>> orderBy;

            switch (sortBy.Trim().ToLower())
            {
                case ConstCorporation:
                    orderBy = q => (sortAscending)
                        ? q.OrderBy(c => c.Corporation.Name).ThenBy(c => c.CharacterName)
                        : q.OrderByDescending(c => c.Corporation.Name).ThenByDescending(c => c.CharacterName);
                    break;

                default:
                    orderBy = q => (sortAscending)
                        ? q.OrderBy(c => c.CharacterName).ThenBy(c => c.Corporation.Name)
                        : q.OrderByDescending(c => c.CharacterName).ThenByDescending(c => c.Corporation.Name);
                    break;
            }

            return orderBy;
        }
    }
}