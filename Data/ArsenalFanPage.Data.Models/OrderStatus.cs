namespace ArsenalFanPage.Data.Models
{
    using System.Collections.Generic;

    using ArsenalFanPage.Data.Common.Models;

    public class OrderStatus : BaseDeletableModel<int>
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
