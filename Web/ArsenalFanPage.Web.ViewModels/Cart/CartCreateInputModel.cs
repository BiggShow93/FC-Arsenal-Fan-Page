namespace ArsenalFanPage.Web.ViewModels.Cart
{
    public class CartCreateInputModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
