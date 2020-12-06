namespace ArsenalFanPage.Web.Controllers
{
    using ArsenalFanPage.Services.Data;
    using ArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HistoryController : Controller
    {
        private readonly INewsService newsService;

        public HistoryController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        [HttpGet]
        public IActionResult History(int id = 1)
        {
            const int ItemsPerPage = 2;

            var viewModel = new NewsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumer = id,
                NewsCount = this.newsService.GetCount(),
                News = this.newsService.GetNews<NewsInListViewModel>(id, ItemsPerPage).Where(x => x.CategoryName.ToLower() == "history"),
            };
            return this.View(viewModel);
        }
    }
}
