namespace ArsenalFanPage.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using ArsenalFanPage.Data;
    using ArsenalFanPage.Data.Common.Repositories;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Data;
    using ArsenalFanPage.Web.ViewModels;
    using ArsenalFanPage.Web.ViewModels.Category;
    using ArsenalFanPage.Web.ViewModels.Home;
    using ArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly INewsService newsService;

        public HomeController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult Index()
        {
            var news = this.newsService.GetNews<NewsInListViewModel>();

            var viewModel = new NewsListViewModel
            {
                NewsCount = this.newsService.GetCount(),
                News = news,
            };

            return this.View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult NewsById(int id)
        {
            var news = this.newsService.GetById<SingleNewsViewModel>(id);

            return this.View(news);
        }

    }
}
