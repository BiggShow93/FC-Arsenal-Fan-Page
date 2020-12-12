namespace ArsenalFanPage.Web.Controllers
{
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Data;
    using ArsenalFanPage.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class ProductsController : BaseController
    {
        private readonly IProductCategorieService productCategoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductService productService;
        private readonly IWebHostEnvironment environment;

        public ProductsController(
            IProductCategorieService productCategoriesService,
            UserManager<ApplicationUser> userManager,
            IProductService productService,
            IWebHostEnvironment environment)
        {
            this.productCategoriesService = productCategoriesService;
            this.userManager = userManager;
            this.productService = productService;
            this.environment = environment;
        }

        public IActionResult Shop()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = this.productCategoriesService.GetAll<ProductDropDownViewModel>();
            var viewModel = new ProductCreateInputModel
            {
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.productService.CreateAsync(
                input, user.Id, input.Name, input.Description, input.Price, input.Quantity, input.ProductCategoryId, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {

                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            return this.Redirect("/");
        }

        public IActionResult ProductById()
        {
            return this.View();
        }
    }
}
