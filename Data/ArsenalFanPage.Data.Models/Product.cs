namespace ArsenalFanPage.Data.Models
{
    using System;

    using ArsenalFanPage.Data.Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public int ImageId { get; set; }

        public virtual Image Image { get; set; }

        public int ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
