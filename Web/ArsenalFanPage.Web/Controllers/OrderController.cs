using ArsenalFanPage.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace ArsenalFanPage.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return this.View();
        }
    }
}
