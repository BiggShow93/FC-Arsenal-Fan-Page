namespace ArsenalFanPage.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ArsenalFanPage.Common;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Data;
    using ArsenalFanPage.Web.ViewModels.Orders;
    using ArsenalFanPage.Web.ViewModels.Product;
    using ArsenalFanPage.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseController
    {
        private readonly IProductCategorieService productCategoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductService productService;
        private readonly IWebHostEnvironment environment;
        private readonly IOrderService orderService;

        public ProductsController(
            IProductCategorieService productCategoriesService,
            UserManager<ApplicationUser> userManager,
            IProductService productService,
            IWebHostEnvironment environment,
            IOrderService orderService)
        {
            this.productCategoriesService = productCategoriesService;
            this.userManager = userManager;
            this.productService = productService;
            this.environment = environment;
            this.orderService = orderService;
        }

        [HttpPost(Name = "Order")]
        public async Task<IActionResult> Order(ProductOrderInputModel input)
        {
            var orderCreateViewModel = new OrderCreateViewModel
            {
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                UserId = this.userManager.GetUserId(this.User),
            };

            await this.orderService.CreateOrder(orderCreateViewModel);

            return this.Redirect("/Orders/Cart");
        }

        public IActionResult Shop(int id = 1)
        {
            const int ItemsPerPage = 8;

            var products = this.productService.GetProducts<ProductInListViewModel>(id, ItemsPerPage);

            var viewModel = new ProductListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumer = id,
                Count = this.productService.GetCount(),
                Products = products,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(ProductCreateInputModel input)
        {
 
            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.productService.CreateAsync(
                input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            return this.Redirect("/Products/Shop");
        }

        public IActionResult ProductById(string id)
        {
            var news = this.productService.GetById<SingleProductViewModel>(id);

            return this.View(news);
        }
    }
}
