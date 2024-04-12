using Microsoft.AspNetCore.Mvc;

namespace PieShopApi.Models.Pies
{
    public class PieListParameters
    {
        const int maxPageSize = 20;
        /// <summary>
        /// The page to return
        /// </summary>
        [FromQuery(Name = "page")]
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        /// <summary>
        /// the size of the page
        /// </summary>
        [FromQuery(Name ="size")]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        [FromQuery(Name = "search")]
        public string? SearchTerm { get; set; }

        [FromQuery(Name = "category")]
        public string? Category { get; set; }
    }

}
