namespace ArsenalFanPage.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ArsenalFanPage.Data;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Web.ViewModels.Orders;
    using ArsenalFanPage.Services.Mapping;

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> CompleteOrder(string orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateOrder(OrderCreateViewModel input)
        {
            var order = new Order
            {
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                UserId = input.UserId,
                OrderStatusId = input.OrderStatusId,
            };

            order.Status = this.dbContext.OrderStatuses
                .SingleOrDefault(orderStatus => orderStatus.Name == "Active");

            this.dbContext.Orders.Add(order);
            int result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }

        public IEnumerable<OrderCreateViewModel> GetAll()
        {
            var orders = this.dbContext.Orders
                 .OrderByDescending(x => x.CreatedOn)
                 .To<OrderCreateViewModel>()
                 .ToList();

            return orders;
        }

        public Task<bool> IncreaseQuantity(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReduceQuantity(string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
