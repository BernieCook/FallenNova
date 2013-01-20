using FallenNova.DomainModel;
using FallenNova.Repository;
using FallenNova.Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FallenNova.Service
{
    public class ContactUsService : BaseService, IContactUsService
    {
        private readonly IGenericRepository<ContactUsLog> _contactUsLogRepository;
        private readonly IGenericRepository<UserLog> _userLogRepository;

        public ContactUsService(
            IUnitOfWork unitOfWork,
            IGenericRepository<ContactUsLog> contactUsLogRepository,
            IGenericRepository<UserLog> userLogRepository)
        {
            UnitOfWork = unitOfWork;
            _contactUsLogRepository = contactUsLogRepository;
            _userLogRepository = userLogRepository;
        }

        #region Insert Methods

        public bool Insert(
            ContactUsDetailsDto contactUsDetailsDto,
            int? addedByUserId,
            ref IList<string> errorMessages)
        {
            if (!contactUsDetailsDto.IsValid)
            {
                errorMessages = contactUsDetailsDto.ErrorMessages.ToList();
                return false;
            }

            var contactUsLog = new ContactUsLog
            {
                Name = contactUsDetailsDto.Name,
                EmailAddress = contactUsDetailsDto.EmailAddress,
                Message = contactUsDetailsDto.Message,
                AddedByUserId = addedByUserId,
                AddedDateTime = DateTime.Now.ToGmtDateTime()
            };

            _contactUsLogRepository.Insert(contactUsLog);

            if (addedByUserId.HasValue)
            {
                var userLog = new UserLog
                {
                    UserId = addedByUserId.Value,
                    UserLogTypeId = (int)UserLogType.Types.SubmittedContactUsMessage,
                    AddedDateTime = DateTime.Now.ToGmtDateTime()
                };

                _userLogRepository.Insert(userLog);
            }

            IEmail email = new Email();
            foreach (var contactUsEmailAddress in email.ContactUsEmailAddresses)
            {
                email = new Email
                {
                    ToEmailAddress = contactUsEmailAddress,
                    ToRecipientName = (!string.IsNullOrWhiteSpace(contactUsDetailsDto.Name) ? contactUsDetailsDto.Name : "Unknown"),
                    Subject = "Contact Us - Fallen Nova",
                    EmailBody = contactUsDetailsDto.Message
                };

                if (email.Dispatch())
                {
                    continue;
                }

                errorMessages.Add(string.Format("The contact us message wasn't sent successfully. Please contact the website administrator."));
                return false;
            }

            UnitOfWork.Commit();

            return true;
        }

        #endregion
    }
}
