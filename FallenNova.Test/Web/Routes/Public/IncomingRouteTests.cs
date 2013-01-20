using FallenNova.Web;
using Moq;
using NUnit.Framework;
using System.Web;
using System.Web.Routing;

namespace FallenNova.Test.Web.Routes.Public
{
    [TestFixture]
    [Category("Routes")]
    public class IncomingRouteTests
    {
        [Ignore]
        public void TestCertainRoute()
        {
            var routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var mockHttpContext = MakeMockHttpContext("~/");
            var routeData = routes.GetRouteData(mockHttpContext.Object);
            Assert.IsNotNull(routeData, "NULL RouteData was returned");
        }

        [Ignore]
        public static Mock<HttpContextBase> MakeMockHttpContext(string url)
        {
            var mockHttpContext = new Mock<HttpContextBase>();

            // Mock the request. 
            var mockRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);

            // Mock the response.
            var mockResponse = new Mock<HttpResponseBase>();
            mockHttpContext.Setup(x => x.Response).Returns(mockResponse.Object);
            mockResponse.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>()))
            .Returns<string>(x => x);
            return mockHttpContext;
        }
    }
}
