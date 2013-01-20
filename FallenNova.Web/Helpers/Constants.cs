using System.Globalization;

namespace FallenNova.Web.Constants
{
    internal class ControllerNamespaces
    {
        internal const string Secure = "FallenNova.Web.Areas.Secure.Controllers";
        internal const string Public = "FallenNova.Web.Areas.Public.Controllers";
    }

    internal class CacheKeyNames
    {
        internal const string Identity = "Identity_";
    }

    public class Format
    {
        public const string Integer = "#,###";
        public const string Security = "0.0";
        public static CultureInfo English = new CultureInfo("en-GB");
    }

    public class Roles
    {
        public const string Member = "Member";
        public const string Administrator = "Administrator";
    }

    public class StringLength
    {
        public const int EmailAddress = 200;
        public const int Password = 50;
    }
    
    public class RouteNames
    {
        public class Secure
        {
            public const string Default = "Secure.Default";
            public const string Data = "Secure.Data";
        }
    }

    public class RouteUrls
    {
        public class Secure
        {
            public const string Default = "secure/{controller}/{action}/{id}";
            public const string Data = "secure/{controller}/{pageIndex}/{pageSize}/{sortBy}/{sortAscending}";
        }
    }

    internal class Separators
    {
        internal const char Comma = ',';
    }
}