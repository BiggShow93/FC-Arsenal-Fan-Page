namespace ArsenalFanPage.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ArsenalFanPage.Data;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Data.Repositories;
    using ArsenalFanPage.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoriesServiceTest
    {
        [Fact]
        public void Get_All_Categoeies_WithParameters_ShouldSuccessfullyCreate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Category {Id = 2, Name = "Top-News" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var categoriesService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);

            var categories = categoriesService.GetAll<MyTestCategory>(1);

            Assert.Single(categories);
        }

        [Fact]
        public void Get_All_Categoeies_WithoutParameters_ShouldSuccessfullyCreate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Category { Id = 2, Name = "Top-News" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var categoriesService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestCategory).Assembly);

            var categories = categoriesService.GetAll<MyTestCategory>(1);

            Assert.Single(categories);
        }

        public class MyTestCategory : IMapFrom<Category>
        {
            public string Name { get; set; }
        }
    }
}
