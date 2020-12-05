namespace ArsenalFanPage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TableController : Controller
    {
        [HttpGet("/Table")]
        public IActionResult Table()
        {
            return this.View();
        }
    }
}
