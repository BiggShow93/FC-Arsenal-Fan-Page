namespace ArsenalFanPage.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ArsenalFanPage.Data.Models;

    public class ProductCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ProductCategories.Any())
            {
                return;
            }

            var categories = new List<string>
            { "T-Shirt", "Jackets", "Polo-Shirts", "Trousers & Shorts", "Hats & Caps", "Git and Accessories", "Sunglasses" };

            foreach (var category in categories)
            {
                await dbContext.ProductCategories.AddAsync(new ProductCategory
                {
                    Name = category,
                });
            }
        }
    }
}
