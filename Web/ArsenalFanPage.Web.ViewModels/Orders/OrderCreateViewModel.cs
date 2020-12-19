namespace ArsenalFanPage.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;

    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;
    using ArsenalFanPage.Web.ViewModels.Product;

    public class OrderCreateViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }
        public string ProductId { get; set; }

        public ProductInListViewModel Product { get; set; }

        public int Quantity { get; set; }

        public string UserId { get; set; }

        public int OrderStatusId { get; set; }

    }
}
