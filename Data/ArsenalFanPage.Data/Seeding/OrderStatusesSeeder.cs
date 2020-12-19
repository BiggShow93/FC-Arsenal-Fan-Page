namespace ArsenalFanPage.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ArsenalFanPage.Data.Models;

    public class OrderStatusesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.OrderStatuses.Any())
            {
                return;
            }

            var statuses = new List<string>
            { "Active", "Completed" };

            foreach (var status in statuses)
            {
                await dbContext.OrderStatuses.AddAsync(new OrderStatus
                {
                    Name = status,
                });
            }
        }
    }
}
