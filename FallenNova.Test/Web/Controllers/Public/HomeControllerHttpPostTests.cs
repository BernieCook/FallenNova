using FallenNova.Service;
using FallenNova.Web.Areas.Public.Controllers;
using FallenNova.Web.Areas.Public.Models;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace FallenNova.Test.Web.Controllers.Public
{
    [TestFixture]
    [Category("Controllers.Public")]
    public class HomeControllerHttpPostTests
    {
        private HomeController _homeController;

        private Mock<IAuthenticateService> _mockAuthenticateService;
        private Mock<IContactUsService> _mockContactUsService;
        private Mock<IUserLogService> _mockUserLogService;

        [SetUp]
        public void SetUp()
        {
            _mockAuthenticateService = new Mock<IAuthenticateService>();
            _mockContactUsService = new Mock<IContactUsService>();
            _mockUserLogService = new Mock<IUserLogService>();

            _homeController = new HomeController(
                _mockAuthenticateService.Object,
                _mockContactUsService.Object,
                _mockUserLogService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _homeController.Dispose();
        }

        [Test]
        public void Basic_Login_Post()
        {
            var loginModel = new LoginModel
            {
                EmailAddress = null,
                Password = null
            };

            var result = _homeController.Login(loginModel, string.Empty) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, string.Empty);
            Assert.False(_homeController.ModelState.IsValid);
            Assert.AreEqual(_homeController.ModelState[string.Empty].Errors[0].ErrorMessage, "The email address and/or password provided is incorrect.");
        }

        [Test]
        public void Basic_Login_Post_2()
        {
            var loginModel = new LoginModel
            {
                EmailAddress = It.IsAny<string>(),
                Password = It.IsAny<string>()
            };

            var result = _homeController.Login(loginModel, string.Empty) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, string.Empty);
            Assert.False(_homeController.ModelState.IsValid);
            Assert.AreEqual(_homeController.ModelState[string.Empty].Errors[0].ErrorMessage, "The email address and/or password provided is incorrect.");
        }

        [Ignore]
        public void Basic_Login_Post_3()
        {
            _mockAuthenticateService
                .Setup(m => m.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var mockHomeController = new Mock<HomeController>();

            // mockHomeController.Setup(m => m.Login(It.IsAny<string>(), null);

            var loginModel = new LoginModel
            {
                EmailAddress = It.IsAny<string>(),
                Password = It.IsAny<string>()
            };

            var result = _homeController.Login(loginModel, string.Empty) as RedirectToRouteResult;

            // Multiple Asserts to handle a null result.
            Assert.IsNotNull(result);
            Assert.AreEqual("X", result.RouteValues["action"]);
        }

        [Ignore]
        public void Basic_ContactUs_Post()
        {
            //var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            //var mockHttpContext = new Mock<HttpContextBase>();
            //mockHttpContext.Setup(x => x.User).Returns(mockClaimsPrincipal);

            var contactUsModel = new ContactUsModel
            {
                EmailAddress = string.Empty,
                Name = string.Empty,
                Message = string.Empty
            };

            var result = _homeController.ContactUs(contactUsModel) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewName, string.Empty);
            Assert.False(_homeController.ModelState.IsValid);
            Assert.AreEqual(_homeController.ModelState[string.Empty].Errors[0].ErrorMessage, "The email address and/or password provided is incorrect.");
        }
    }
}
