namespace ArsenalFanPage.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int PageNumer { get; set; }

        public int NewsCount { get; set; }

        public bool HasPreviosPage => this.PageNumer > 1;

        public int PreviosPageNumber => this.PageNumer - 1;

        public bool HasNextPage => this.PageNumer < this.PagesCount;

        public int NextPageNumber => this.PageNumer + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.NewsCount / this.ItemsPerPage);

        public int ItemsPerPage { get; set; }
    }
}
