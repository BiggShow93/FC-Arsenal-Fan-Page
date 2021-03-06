﻿namespace ArsenalFanPage.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ArsenalFanPage.Common;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Data;
    using ArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : BaseController
    {
        private readonly INewsService newsService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public NewsController(
            INewsService newsService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.newsService = newsService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult News()
        {

            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new NewsCreateInputModel
            {
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]

        public async Task<IActionResult> Create(NewsCreateInputModel input)
        {

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.newsService.CreateAsync(
               input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            return this.Redirect("/");
        }
    }
}
