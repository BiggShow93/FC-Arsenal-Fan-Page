namespace ArsenalFanPage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        public IActionResult ByName(string name)
        {
            return this.View();
        }
    }
}
