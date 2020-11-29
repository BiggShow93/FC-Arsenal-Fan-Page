namespace ArsenalFanPage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        public IActionResult ByName(string name)
        {
            return this.View();
        }
    }
}
