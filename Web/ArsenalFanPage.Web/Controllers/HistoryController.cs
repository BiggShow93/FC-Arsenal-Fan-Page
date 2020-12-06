namespace ArsenalFanPage.Web.Controllers
{
    using ArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;

    public class HistoryController : Controller
    {
        [HttpGet("/History")]
        public IActionResult History(int id)
        {
            var viewModel = new NewsListViewModel
            {
                PageNumer = id,
            };
            return this.View();
        }
    }
}
