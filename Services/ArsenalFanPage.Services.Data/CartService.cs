namespace ArsenalFanPage.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using ArsenalFanPage.Web.ViewModels.Cart;

    public class CartService : ICartService
    {
        public Task AddToCartAsync(CartCreateInputModel input, int productId, string productName, string userId, int quantity, decimal price)
        {
            throw new NotImplementedException();
        }
    }
}
