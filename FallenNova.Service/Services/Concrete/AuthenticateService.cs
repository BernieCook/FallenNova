using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading;
using FallenNova.Shared.ExtensionMethods;

namespace FallenNova.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private const string ConstSecurityExceptionMessage = "More than one active user with the same email address.";

        private readonly IUserService _userService;
        private readonly IUserLogService _userLogService;

        public AuthenticateService(
            IUserService userService,
            IUserLogService userLogService)
        {
            _userService = userService;
            _userLogService = userLogService;
        }

        public bool ValidateUser(
            string emailAddress,
            string password)
        {
            int? userId = null;

            IList<UserAuthenticationDetailsDto> userAuthenticationDetailsDtos = _userService.GetByEmailAddress(emailAddress).ToList();

            if (userAuthenticationDetailsDtos.Count(u => u.IsActive) == 1)
            {
                var userAuthenticationDetailsDto = userAuthenticationDetailsDtos.Single(u => u.IsActive);

                if (_userService.VerifyPassword(password, userAuthenticationDetailsDto.HashedPassword))
                {
                    userId = userAuthenticationDetailsDto.UserId;
                }
            }
            else
            {
                // There is more than one active user with the supplied credentials.
                throw new SecurityException(ConstSecurityExceptionMessage);
            }

            if (!userId.HasValue)
            {
                Thread.CurrentPrincipal = null;
                return false;
            }

            SetCurrentPrincipal(userId.Value);

            return true;
        }

        public bool ValidateUser(int userId)
        {
            var userStatusDto = _userService.GetStatus(userId);

            if (!userStatusDto.IsActive)
            {
                Thread.CurrentPrincipal = null;
                return false;
            }

            SetCurrentPrincipal(userId);

            _userLogService.AuthenticatedSuccessfully(userId);

            return true;
        }

        public void SetCurrentPrincipal(int userId)
        {
            if (!(Thread.CurrentPrincipal is System.Security.Principal.GenericPrincipal))
            {
                return;
            }

            var userIdentityDetailsDto = _userService.GetIdentityDetails(userId);

            var customClaimsIdentity = new CustomClaimsIdentity(
                userIdentityDetailsDto.UserId,
                userIdentityDetailsDto.FirstName,
                userIdentityDetailsDto.Surname,
                userIdentityDetailsDto.EmailAddress,
                userIdentityDetailsDto.Roles.Select(r => r.Title).ToList());

            var claimsPrincipal = new ClaimsPrincipal(customClaimsIdentity);

            Thread.CurrentPrincipal = claimsPrincipal;
        }

        public void Dispose()
        {
            _userService.Dispose();
        }
    }

    /// <summary>
    /// Custom claims-based identity (using .NET 4.5).
    /// </summary>
    /// <remarks>
    /// If this identity is being consumed by a MVC 4 application ensure the following code is inserted into the Application_Start() method.
    /// AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name;
    /// </remarks>
    [Serializable]
    public sealed class CustomClaimsIdentity : ClaimsIdentity
    {
        private const string ConstAuthenticationType = "Custom Identity";
        private const string ConstAdministrator = "Administrator";

        private readonly bool _isAuthenticated;

        public CustomClaimsIdentity(
            int userId,
            string firstName,
            string surname,
            string emailAddress,
            IEnumerable<string> roles)
        {
            AddClaim(new Claim(ClaimTypes.Name, userId.ToString(CultureInfo.CurrentCulture)));
            AddClaim(new Claim(ClaimTypes.GivenName, firstName));
            AddClaim(new Claim(ClaimTypes.Surname, surname));
            AddClaim(new Claim(ClaimTypes.Email, emailAddress));

            foreach (var role in roles)
            {
                AddClaim(new Claim(ClaimTypes.Role, role));
            }

            _isAuthenticated = true;
        }

        public int UserId
        {
            get { return Int32.Parse(FindFirst(ClaimTypes.Name).Value); }
        }

        public string FirstName
        {
            get { return FindFirst(ClaimTypes.GivenName).Value; }
        }

        public string Surname
        {
            get { return FindFirst(ClaimTypes.Surname).Value; }
        }

        public string FullName
        {
            get { return string.Concat(FirstName, " ", Surname); }
        }

        public string EmailAddress
        {
            get { return FindFirst(ClaimTypes.Email).Value; }
        }

        public IEnumerable<string> Roles
        {
            get { return FindAll(ClaimTypes.Role).Select(s => s.Value); }
        }

        public bool IsAdministrator
        {
            get { return (Roles.Any(r => r.EqualsCaseInsensitive(ConstAdministrator))); }
        }

        public override string AuthenticationType
        {
            get { return ConstAuthenticationType; }
        }

        public override bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }
    }
}
