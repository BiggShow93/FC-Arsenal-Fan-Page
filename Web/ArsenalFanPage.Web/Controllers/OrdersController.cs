namespace ArsenalFanPage.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    using ArsenalFanPage.Services.Data;
    using ArsenalFanPage.Services.Mapping;
    using ArsenalFanPage.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var orders = this.orderService.GetAll();

            var viewModel = new OrderCartViewModel
            {
                Ordes = (IEnumerable<OrderCreateViewModel>)orders,
            };

            return this.View(viewModel);

        }
    }
}
