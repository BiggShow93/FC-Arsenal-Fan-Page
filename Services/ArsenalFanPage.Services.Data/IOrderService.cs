namespace ArsenalFanPage.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Web.ViewModels.Orders;

   public interface IOrderService
    {
        Task<bool> CreateOrder(OrderCreateViewModel orderServiceModel);

        IEnumerable<OrderCreateViewModel> GetAll();

        //Task SetOrdersToReceipt(Receipt receipt);

        Task<bool> CompleteOrder(string orderId);

        Task<bool> ReduceQuantity(string orderId);

        Task<bool> IncreaseQuantity(string orderId);
    }
}
