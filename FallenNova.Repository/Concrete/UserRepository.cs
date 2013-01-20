using FallenNova.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FallenNova.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private const string ConstSurname = "surname";

        public UserRepository(FallenNovaContext context) 
            : base(context)
        {
        }

        public IQueryable<User> GetByEmailAddress(string emailAddress)
        {
            return Get(
                u => u.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower());
        }

        public async Task<IEnumerable<User>> GetLatestAsync(
            int takeCount)
        {
            return await GetAsync(
                takeCount,
                q => q.OrderBy(u => u.AddedDateTime));
        }

        public IEnumerable<User> GetAll(
            int skipCount,
            int takeCount,
            string sortBy,
            bool sortAscending,
            out int totalCount)
        {
            return GetAll(
                out totalCount,
                skipCount,
                takeCount,
                orderBy: GetOrderBy(sortBy, sortAscending));
        }

        private Func<IQueryable<User>, IOrderedQueryable<User>> GetOrderBy(
            string sortBy,
            bool sortAscending)
        {
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy;

            switch (sortBy.Trim().ToLower())
            {
                case ConstSurname:
                    orderBy = q => (sortAscending)
                        ? q.OrderBy(u => u.Surname).ThenBy(u => u.FirstName)
                        : q.OrderByDescending(u => u.Surname).ThenByDescending(u => u.FirstName);
                    break;

                default:
                    orderBy = q => (sortAscending)
                        ? q.OrderBy(u => u.FirstName).ThenBy(u => u.Surname)
                        : q.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.Surname);
                    break;
            }

            return orderBy;
        }
    }
}