namespace ArsenalFanPage.Services.Data
{
    using System.Threading.Tasks;

    using ArsenalFanPage.Web.ViewModels.Cart;

    public interface ICartService
    {
        Task AddToCartAsync(CartCreateInputModel input, int productId, string productName, string userId, int quantity, decimal price);
    }
}
