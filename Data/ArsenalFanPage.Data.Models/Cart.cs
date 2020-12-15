using ArsenalFanPage.Data.Common.Models;

namespace ArsenalFanPage.Data.Models
{
    public class Cart : BaseDeletableModel<int>
    {
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
