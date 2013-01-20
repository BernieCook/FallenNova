using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FallenNova.DomainModel;
using FallenNova.Repository;
using FallenNova.Shared.ExtensionMethods;

namespace FallenNova.Service
{
    public class UserService : BaseService, IUserService
    {
        #region Constants

        // NOTE: Don't reduce the work factor, it won't support existing hashed passwords. Upping the work factor 
        // is fine however, just remember there will be a slight increase in the time it takes to generate the hash.
        private const int ConstBCryptWorkFactor = 12;

        #endregion

        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IGenericRepository<UserLog> _userLogRepository;

        public UserService(
            IUnitOfWork unitOfWork,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            IGenericRepository<UserLog> userLogRepository)
        {
            UnitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _userLogRepository = userLogRepository;
        }

        #region Get Methods

        public UserDetailsDto GetDetails(
            int userId,
            bool isGetDeep = false)
        {
            var user = (!isGetDeep)
                ? _userRepository.GetById(userId)
                : _userRepository.Get(
                    u => u.UserId == userId,
                    u => u.Characters,
                    u => u.Characters.Select(c => c.Corporation),
                    u => u.UserRoles.Select(ur => ur.Role));

            return new UserDetailsDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                Surname = user.Surname,
                EmailAddress = user.EmailAddress,
                Characters = user.Characters.Select(c =>
                    new CharacterDetailsDto
                    {
                        CharacterId = c.CharacterId,
                        CharacterName = c.CharacterName,
                        CorporationName = c.Corporation.Name
                    }).ToList(),
                Roles = user.UserRoles.Select(ur =>
                    new RoleDetailsDto
                    {
                        RoleId = ur.Role.RoleId,
                        Title = ur.Role.Title
                    }).ToList()
            };
        }

        public UserIdentityDetailsDto GetIdentityDetails(int userId)
        {
            var user = _userRepository.Get(
                u => u.UserId == userId,
                u => u.UserRoles.Select(ur => ur.Role));

            return new UserIdentityDetailsDto
            {
                UserId = user.UserId,
                UserStatusId = user.UserStatusId,
                FirstName = user.FirstName,
                Surname = user.Surname,
                EmailAddress = user.EmailAddress,
                Roles = user.UserRoles.Select(ur => ur.Role).Select(r =>
                    new RoleDetailsDto
                    {
                        RoleId = r.RoleId,
                        Title = r.Title
                    })
            };
        }

        public UserContactDetailsDto GetContactDetails(int userId)
        {
            var user = _userRepository.Get(
                u => u.UserId == userId,
                u => u.UserRoles.Select(ur => ur.Role));

            return new UserContactDetailsDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                Surname = user.Surname,
                EmailAddress = user.EmailAddress,
                // NOTE: This configuration only supports a single role per user, although the architecture supports 
                // multiple roles per user which is due to be implemented shortly.
                RoleId = user.UserRoles.Select(ur => ur.Role).First().RoleId
            };
        }

        public UserStatusDetailsDto GetStatusDetails(int userId)
        {
            var user = _userRepository.GetById(
                userId);

            return new UserStatusDetailsDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                Surname = user.Surname,
                UserStatusId = user.UserStatusId
            };
        }

        public UserStatusDto GetStatus(int userId)
        {
            var user = _userRepository.GetById(userId);

            return new UserStatusDto
            {
                UserId = user.UserId,
                UserStatusId = user.UserStatusId,
                IsActive = (user.UserStatusId == (int)UserStatus.Statuses.Active)
            };
        }

        public IEnumerable<UserAuthenticationDetailsDto> GetByEmailAddress(string emailAddress)
        {
            return _userRepository.GetByEmailAddress(emailAddress).Select(u =>
                new UserAuthenticationDetailsDto
                {
                    UserId = u.UserId,
                    UserStatusId = u.UserStatusId,
                    EmailAddress = u.EmailAddress,
                    HashedPassword = u.HashedPassword,
                    IsActive = (u.UserStatusId == (int)UserStatus.Statuses.Active)
                });
        }

        public IEnumerable<UserDetailsDto> GetUsers(
            int pageIndex,
            int pageSize,
            string sortBy,
            bool sortAscending,
            out int totalUserCount)
        {
            return _userRepository.GetAll(
                ((pageIndex - 1) * pageSize),
                pageSize,
                sortBy,
                sortAscending,
                out totalUserCount).Select(u =>
                new UserDetailsDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    Surname = u.Surname
                });
        }

        public async Task<IEnumerable<UserDetailsDto>> GetLatestUsersAsync(
            int totalUsers)
        {
            var users = await _userRepository.GetLatestAsync(
                totalUsers);

            return users.Select(u =>
                new UserDetailsDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    Surname = u.Surname
                });
        }

        #endregion

        #region Insert Methods

        public bool Insert(
            UserContactDetailsDto userContactDetailsDto,
            int addedByUserId,
            ref IList<string> errorMessages)
        {
            #region Validation

            if (!userContactDetailsDto.IsValid)
            {
                errorMessages = userContactDetailsDto.ErrorMessages.ToList();
                return false;
            }

            if (_roleRepository.GetByUserId(addedByUserId).All(r => r.RoleId != (int)Role.Roles.Administrator))
            {
                errorMessages.Add("Stop! You can't add a new user unless you're an administrator.");
            }

            if (errorMessages.Count > 0)
            {
                return false;
            }

            #endregion

            // Prepare the user entity.
            var addedDateTime = DateTime.Now.ToGmtDateTime();
            var user = new User
            {
                UserStatusId = (int)UserStatus.Statuses.Active,
                FirstName = userContactDetailsDto.FirstName,
                Surname = userContactDetailsDto.Surname,
                EmailAddress = userContactDetailsDto.EmailAddress,
                HashedPassword = null,
                UnsuccessfulLoginAttempts = 0,
                AddedByUserId = addedByUserId,
                AddedDateTime = addedDateTime,
                ModifiedByUserId = addedByUserId,
                ModifiedDateTime = addedDateTime
            };

            _userRepository.Insert(user);

            var userRole = new UserRole
            {
                UserId = user.UserId,
                RoleId = userContactDetailsDto.RoleId
            };

            _userRoleRepository.Insert(userRole);

            var userLog = new UserLog
            {
                UserId = addedByUserId,
                UserLogTypeId = (int)UserLogType.Types.AddedUser,
                ActionAgainstUserId = userContactDetailsDto.UserId,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _userLogRepository.Insert(userLog);

            // Dispatch email.
            IEmail email = new Email
            {
                ToEmailAddress = userContactDetailsDto.EmailAddress,
                ToRecipientName = string.Format("{0} {1}", userContactDetailsDto.FirstName, userContactDetailsDto.Surname),
                Subject = "Verify Email Address - Fallen Nova",
                EmailBody = "Email body goes here"
            };

            if (!email.Dispatch())
            {
                errorMessages.Add(string.Format("The user's account was created however the verification email wasn't sent succesfully. Contact the web admin."));
                return false;
            }

            userContactDetailsDto.UserId = user.UserId;

            UnitOfWork.Commit();

            return true;
        }

        #endregion

        #region Update Methods

        public bool UpdateContactDetails(
            UserContactDetailsDto userContactDetailsDto,
            int modifiedByUserId,
            ref IList<string> errorMessages)
        {
            var user = _userRepository.GetById(userContactDetailsDto.UserId);

            #region Validation

            if (!userContactDetailsDto.IsValid)
            {
                errorMessages = userContactDetailsDto.ErrorMessages.ToList();
            }

            if (user.UserStatusId != (int)UserStatus.Statuses.Active)
            {
                errorMessages.Add("You can't edit a user whose account is disabled or deleted.");
            }

            if ((userContactDetailsDto.UserId != modifiedByUserId) &&
                (_roleRepository.GetByUserId(modifiedByUserId).All(r => r.RoleId != (int)Role.Roles.Administrator)))
            {
                errorMessages.Add("Wait, you're attempting to update another user's contact details. You can only edit your details, unless you're an administrator.");
            }

            if (errorMessages.Count > 0)
            {
                return false;
            }

            #endregion

            user.FirstName = userContactDetailsDto.FirstName;
            user.Surname = userContactDetailsDto.Surname;
            user.EmailAddress = userContactDetailsDto.EmailAddress;
            user.ModifiedByUserId = modifiedByUserId;
            user.ModifiedDateTime = DateTime.Now.ToGmtDateTime();

            _userRepository.Update(user);

            var newUserRole = new UserRole
            {
                UserId = user.UserId,
                RoleId = userContactDetailsDto.RoleId
            };

            _userRoleRepository.DeleteByUserId(user.UserId);
            _userRoleRepository.Insert(newUserRole);

            var userLog = new UserLog
            {
                UserId = modifiedByUserId,
                UserLogTypeId = (int)UserLogType.Types.EditedUserContactDetails,
                ActionAgainstUserId = userContactDetailsDto.UserId,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _userLogRepository.Insert(userLog);
            UnitOfWork.Commit();

            return true;
        }

        public bool UpdatePassword(
            UserPasswordDto userPasswordDto,
            int modifiedByUserId,
            ref IList<string> errorMessages)
        {
            var user = _userRepository.GetById(userPasswordDto.UserId);

            #region Validation

            if (!userPasswordDto.IsValid)
            {
                errorMessages = userPasswordDto.ErrorMessages.ToList();
            }

            if (!VerifyPassword(userPasswordDto.CurrentPassword, user.HashedPassword))
            {
                errorMessages.Add("The current password supplied doesn't match what's in the database.");
            }

            if (userPasswordDto.UserId != modifiedByUserId)
            {
                errorMessages.Add("Wo wo wo, you're attempting to update another user's password. You can only edit your own password.");
            }

            if (errorMessages.Count > 0)
            {
                return false;
            }

            #endregion

            user.HashedPassword = HashPassword(userPasswordDto.NewPassword);
            user.ModifiedByUserId = modifiedByUserId;
            user.ModifiedDateTime = DateTime.Now.ToGmtDateTime();

            _userRepository.Update(user);

            var userLog = new UserLog
            {
                UserId = modifiedByUserId,
                UserLogTypeId = (int)UserLogType.Types.EditedUserPassword,
                ActionAgainstUserId = userPasswordDto.UserId,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _userLogRepository.Insert(userLog);
            UnitOfWork.Commit();

            return true;
        }

        public bool UpdateStatus(
            UserStatusDetailsDto userStatusDetailsDto,
            int modifiedByUserId,
            ref IList<string> errorMessages)
        {
            var user = _userRepository.GetById(userStatusDetailsDto.UserId);

            #region Validation

            if (_roleRepository.GetByUserId(modifiedByUserId).All(r => r.RoleId != (int)Role.Roles.Administrator))
            {
                errorMessages.Add("Wo wo wo, you're attempting to update another user's status details. You're not allowed to unless you're an administrator.");
            }

            if (errorMessages.Count > 0)
            {
                return false;
            }

            #endregion

            user.UserStatusId = userStatusDetailsDto.UserStatusId;
            user.ModifiedByUserId = modifiedByUserId;
            user.ModifiedDateTime = DateTime.Now.ToGmtDateTime();

            _userRepository.Update(user);

            var userLog = new UserLog
            {
                UserId = modifiedByUserId,
                UserLogTypeId = (int)UserLogType.Types.EditedUserStatus,
                ActionAgainstUserId = userStatusDetailsDto.UserId,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _userLogRepository.Insert(userLog);
            UnitOfWork.Commit();

            return true;
        }

        #endregion

        #region Security Methods

        public string HashPassword(
            string password)
        {
            // I'm not including a salt in the password hashing. This process takes a fraction longer but it's 
            // more robust against brute force or dictionary attacks. 
            return BCrypt.Net.BCrypt.HashPassword(password, ConstBCryptWorkFactor);
        }

        public bool VerifyPassword(
            string password,
            string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        #endregion
    }
}
