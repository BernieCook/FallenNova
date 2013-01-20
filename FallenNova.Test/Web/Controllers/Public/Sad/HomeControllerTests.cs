using FallenNova.DomainModel;
using FallenNova.Service;
using FallenNova.Web.Areas.Public.Controllers;
using NUnit.Framework;
using Ninject;
using System.Web.Mvc;

namespace FallenNova.Test.Web.Controllers.Public.Sad
{
    [TestFixture]
    [Category("Controllers.Public")]
    public class HomeControllerTests
    {
        private HomeController _homeController;

        private readonly IKernel _kernel;

        public HomeControllerTests()
        {
            _kernel = new StandardKernel();

            _kernel.Bind<IUnitOfWork>().ToMethod(ctx => ctx.Kernel.Get<FallenNovaContext>());

            _kernel.Bind<IAuthenticateService>().To<AuthenticateService>();
            _kernel.Bind<IContactUsService>().To<ContactUsService>();
            _kernel.Bind<IUserService>().To<UserService>();
            _kernel.Bind<IUserLogService>().To<UserLogService>();
        }

        [SetUp]
        public void SetUp()
        {
            _homeController = _kernel.Get<HomeController>();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void HomeController_Index_View_HttpGet()
        {
            var expectedViewName = string.Empty;

            var result = _homeController.Login() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

    }
}
