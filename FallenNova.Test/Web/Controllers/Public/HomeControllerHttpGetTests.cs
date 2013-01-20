using FallenNova.Service;
using FallenNova.Web.Areas.Public.Controllers;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace FallenNova.Test.Web.Controllers.Public
{
    [TestFixture]
    [Category("Controllers.Public")]
    public class HomeControllerHttpGetTests
    {
        private HomeController _homeController;

        [SetUp]
        public void SetUp()
        {
            var mockAuthenticateService = new Mock<IAuthenticateService>();
            var mockContactUsService = new Mock<IContactUsService>();
            var mockUserLogService = new Mock<IUserLogService>();

            _homeController = new HomeController(
                mockAuthenticateService.Object,
                mockContactUsService.Object,
                mockUserLogService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _homeController.Dispose();
        }

        [Test]
        public void Basic_AboutUs_Get()
        {
            var expectedViewName = string.Empty;

            var result = _homeController.AboutUs() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public void Basic_Error_Get()
        {
            var expectedViewName = string.Empty;

            var result = _homeController.Error() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public void Basic_Index_Get()
        {
            var expectedViewName = string.Empty;

            var result = _homeController.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public void Basic_Join_Get()
        {
            var expectedViewName = string.Empty;

            var result = _homeController.Join() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public void Basic_Login_Get()
        {
            var expectedViewName = string.Empty;

            var result = _homeController.Login() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }
    }
}
