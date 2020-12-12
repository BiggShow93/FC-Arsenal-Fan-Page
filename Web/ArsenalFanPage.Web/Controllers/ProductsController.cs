namespace ArsenalFanPage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        public IActionResult Shop()
        {
            return this.View();
        }

        public IActionResult ProductById()
        {
            return this.View();
        }
    }
}
