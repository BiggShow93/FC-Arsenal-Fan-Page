namespace ArsenalFanPage.Web.Controllers
{
    using ArsenalFanPage.Services.Data;
    using ArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;

    public class HistoryController : BaseController
    {
        private readonly INewsService newsService;

        public HistoryController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        [HttpGet]
        public IActionResult News(int id = 1)
        {
            const int ItemsPerPage = 3;

            var historyNews = this.newsService.GetNews<NewsInListViewModel>(id, ItemsPerPage);
            var category = "HISTORY";

            var viewModel = new NewsListViewModel
                {
                    ItemsPerPage = ItemsPerPage,
                    PageNumer = id,
                    Count = this.newsService.GetCount(category),
                    News = historyNews,
                };

            return this.View(viewModel);
        }

        public IActionResult NewsById(int id)
        {
            var news = this.newsService.GetById<SingleNewsViewModel>(id);

            return this.View(news);
        }
    }
}
