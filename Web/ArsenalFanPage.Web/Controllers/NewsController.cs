namespace ArsenalFanPage.Web.Controllers
{
    using System.Threading.Tasks;

    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Data;
    using ArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : BaseController
    {
        private readonly INewsService newsService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public NewsController(
            INewsService newsService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.newsService = newsService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        [HttpGet("/News")]
        public IActionResult News()
        {
            return this.View();
        }

        [HttpGet("News/Create")]

        // [Authorize]
        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new NewsCreateInputModel();

            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        [HttpPost("News/Create")]

        // TODO: ADMIN [Authorize]
        public async Task<IActionResult> Create(NewsCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.newsService.CreateAsync(
                input.Title, input.CategoryId, input.Content, user.Id);

            return this.RedirectToAction();
        }
    }
}
