namespace ArsenalFanPage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HistoryController : Controller
    {
        [HttpGet("/History")]
        public IActionResult History()
        {
            return this.View();
        }
    }
}
