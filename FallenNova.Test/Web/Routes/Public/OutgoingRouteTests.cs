using FallenNova.Web;
using NUnit.Framework;
using System.Web.Mvc;
using System.Web.Routing;

namespace FallenNova.Test.Web.Routes.Public
{
    [TestFixture]
    [Category("Routes")]
    internal class OutgoingRouteTests
    {
        [Ignore]
        public void Edit_Products_ID_10_Test()
        {
            var result = SetupUrlViaMocks(
                new { controller = "Products", action = "Edit", id = 10 }
                );
            Assert.AreEqual("/Products/Edit/10", result);
        }

        [Ignore]
        public string SetupUrlViaMocks(object values)
        {
            var routeConfig = new RouteCollection();
            RouteConfig.RegisterRoutes(routeConfig);
            var mockContext = IncomingRouteTests.MakeMockHttpContext(null);
            var context = new RequestContext(mockContext.Object, new RouteData());
            return UrlHelper.GenerateUrl(null, null, null, new RouteValueDictionary(values), routeConfig, context, true);
        }
    }
}
