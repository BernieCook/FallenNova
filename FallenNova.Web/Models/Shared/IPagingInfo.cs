namespace FallenNova.Web.Areas.Shared.Models
{
    public interface IPagingInfo
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalResults { get; set; }
        string SortBy { get; set; }
        bool SortAscending { get; set; }
        string RouteName { get; set; }
        string RouteUrl { get; set; }

        int FooterPageCount { get; }
        int FooterPageEnd { get; }
        int FooterPageStart { get; }
        int PageStart { get; }
        int PageEnd { get; }
        object RouteValuesFirst { get; }
        object RouteValuesPrevious { get; }
        object RouteValuesLast { get; }
        object RouteValuesNext { get; }
        int TotalPages { get; }

        bool IsSortAscending(string sortByMatch);
    }
}