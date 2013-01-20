using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FallenNova.Service
{
    public interface IUserService : IDisposable
    {
        UserDetailsDto GetDetails(int userId, bool isGetDeep = false);
        UserIdentityDetailsDto GetIdentityDetails(int userId);
        UserContactDetailsDto GetContactDetails(int userId);
        UserStatusDetailsDto GetStatusDetails(int userId);
        UserStatusDto GetStatus(int userId);
        IEnumerable<UserAuthenticationDetailsDto> GetByEmailAddress(string emailAddress);
        IEnumerable<UserDetailsDto> GetUsers(int pageIndex, int pageSize, string sortBy, bool sortAscending, out int totalUserCount);

        Task<IEnumerable<UserDetailsDto>> GetLatestUsersAsync(int totalUsers);

        bool Insert(UserContactDetailsDto userContactDetailsDto, int addedByUserId, ref IList<string> errorMessages);

        bool UpdateContactDetails(UserContactDetailsDto userContactDetailsDto, int modifiedByUserId, ref IList<string> errorMessages);
        bool UpdatePassword(UserPasswordDto userPasswordDto, int modifiedByUserId, ref IList<string> errorMessages);
        bool UpdateStatus(UserStatusDetailsDto userStatusDetailsDto, int modifiedByUserId, ref IList<string> errorMessages);

        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
