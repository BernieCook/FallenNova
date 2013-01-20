using System;
using System.Linq;

using FallenNova.DomainModel;
using FallenNova.Repository;
using FallenNova.Shared.ExtensionMethods;

namespace FallenNova.Service
{
    public class UserLogService : BaseService, IUserLogService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<UserLog> _userLogRepository;

        public UserLogService(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IGenericRepository<UserLog> userLogRepository)
        {
            UnitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userLogRepository = userLogRepository;
        }

        public void AuthenticatedSuccessfully(
            int userId)
        {
            var user = _userRepository.GetById(userId);

            user.LastSuccessfulAuthenticationDateTime = DateTime.Now.ToGmtDateTime();
            user.ModifiedDateTime = DateTime.Now.ToGmtDateTime();

            var userLog = new UserLog
            {
                UserId = userId,
                UserLogTypeId = (int)UserLogType.Types.AuthenticatedSuccessfullyAutomaticLogin,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _userRepository.Update(user);
            _userLogRepository.Insert(userLog);
            UnitOfWork.Commit();
        }

        public void LoginSuccessful(
            int userId)
        {
            var user = _userRepository.GetById(userId);

            user.UnsuccessfulLoginAttempts = 0;
            user.LastSuccessfulLoginDateTime = DateTime.Now.ToGmtDateTime();
            user.ModifiedDateTime = DateTime.Now.ToGmtDateTime();

            var userLog = new UserLog
            {
                UserId = userId,
                UserLogTypeId = (int)UserLogType.Types.LoggedInSuccessfullyManualLogin,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _userRepository.Update(user);
            _userLogRepository.Insert(userLog);
            UnitOfWork.Commit();
        }

        public void LoginUnsuccessful(
            string emailAddress)
        {
            var user = _userRepository.GetByEmailAddress(emailAddress).Single();

            user.UnsuccessfulLoginAttempts++;
            user.ModifiedDateTime = DateTime.Now.ToGmtDateTime();

            var userLog = new UserLog
            {
                UserId = user.UserId,
                UserLogTypeId = (int)UserLogType.Types.LoggedInUnsuccessfullyManualLogin,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _userRepository.Update(user);
            _userLogRepository.Insert(userLog);
            UnitOfWork.Commit();
        }

        public void LoggedOut(
            int userId)
        {
            // Prepare the user log.
            var userLog = new UserLog
            {
                UserId = userId,
                UserLogTypeId = (int)UserLogType.Types.LoggedOut,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _userLogRepository.Insert(userLog);
            UnitOfWork.Commit();
        }
    }
}
