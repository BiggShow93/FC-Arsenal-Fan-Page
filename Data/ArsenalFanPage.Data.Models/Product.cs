namespace ArsenalFanPage.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ArsenalFanPage.Data.Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Images = new HashSet<Image>();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public int ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
