using FallenNova.Shared.ExtensionMethods;

namespace FallenNova.Web.Areas.Shared.Models
{
    public class PagingInfo : IPagingInfo
    {
        public const int ConstFooterPageCount = 10;

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
        public string RouteName { get; set; }
        public string RouteUrl { get; set; }

        /// <summary>
        /// The maximum number of footer paging options.
        /// </summary>
        public int FooterPageCount
        {
            get { return ConstFooterPageCount; }
        }

        /// <summary>
        /// The page end index for the footer.
        /// </summary>
        public int FooterPageEnd
        {
            get
            {
                return (FooterPageStart + FooterPageCount - 1 < TotalPages)
                    ? (FooterPageStart + FooterPageCount - 1)
                    : TotalPages;
            }
        }

        /// <summary>
        /// The page start index for the footer.
        /// </summary>
        public int FooterPageStart
        {
            get { return PageIndex - ((PageIndex - 1) % FooterPageCount); }
        }

        /// <summary>
        /// The current page's start index.
        /// </summary>
        public int PageStart
        {
            get
            {
                return (PageIndex == 1)
                    ? 1
                    : (PageIndex - 1) * PageSize + 1;
            }
        }

        /// <summary>
        /// The current page's end index.
        /// </summary>
        public int PageEnd
        {
            get
            {
                return ((PageStart + PageSize) < TotalResults) 
                    ? PageStart + PageSize - 1
                    : TotalResults;
            }
        }

        /// <summary>
        /// Route values for the "First" link.
        /// </summary>
        public object RouteValuesFirst
        {
            get
            {
                return new
                    {
                        pageIndex = 1, 
                        pageSize = PageSize, 
                        sortBy = SortBy, 
                        sortAscending = SortAscending
                    };
            }
        }

        /// <summary>
        /// Route values for the first "..." link.
        /// </summary>
        public object RouteValuesPrevious
        {
            get
            {
                return new
                    {
                        pageIndex = (FooterPageStart - 1), 
                        pageSize = PageSize, 
                        sortBy = SortBy, 
                        sortAscending = SortAscending
                    };
            }
        }

        /// <summary>
        /// Route values for the "Last" link.
        /// </summary>
        public object RouteValuesLast
        {
            get
            {
                return new
                    {
                        pageIndex = TotalPages, 
                        pageSize = PageSize, 
                        sortBy = SortBy, 
                        sortAscending = SortAscending
                    };
            }
        }

        /// <summary>
        /// Route values for the last "..." link.
        /// </summary>
        public object RouteValuesNext
        {
            get
            {
                return new
                    {
                        pageIndex = (FooterPageEnd + 1), 
                        pageSize = PageSize, 
                        sortBy = SortBy, 
                        sortAscending = SortAscending
                    };
            }
        }

        /// <summary>
        /// Total number of pages.
        /// </summary>
        public int TotalPages
        {
            get
            {
                return (TotalResults / PageSize) + 
                    ((TotalResults % PageSize > 0) 
                    ? 1 
                    : 0);
            }
        }

        /// <summary>
        /// Determines if the current sorted field should be ascending or descending.
        /// </summary>
        /// <param name="sortByMatch">Name of field to sort by.</param>
        /// <returns>True to sort ascending, false to sort descending.</returns>
        public bool IsSortAscending(string sortByMatch)
        {
            return (((!string.IsNullOrWhiteSpace(SortBy)) && (!SortBy.EqualsCaseInsensitive(sortByMatch))) || (!SortAscending)) || !SortAscending;
        }
    }
}