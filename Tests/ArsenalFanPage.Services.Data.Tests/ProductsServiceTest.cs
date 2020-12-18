namespace ArsenalFanPage.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ArsenalFanPage.Data;
    using ArsenalFanPage.Data.Common.Repositories;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Data.Repositories;
    using ArsenalFanPage.Services.Mapping;
    using ArsenalFanPage.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ProductsServiceTest
    {
        private List<Product> GetDummyData()
        {
            return new List<Product>()
            {
                new Product
                {
                 Id = "FirstId",
                 Name = "T-Shirt",
                 Price = 78.43M,
                 Quantity = 50,
                 ProductCategory = new ProductCategory{ Name = "T-Shirt"},
                 ProductCategoryId = 6,
                 Image = new Image{RemoteImageUrl = "new/image.jpg" },
                 ImageId = "123456",
                 CreatedByUserId = "ArsenalAdmin",
                 CreatedByUser = new ApplicationUser{UserName = "ArsenalAdmin" },
                },
                new Product
                {
                    Id = "SecondId",
                    Name = "Polo",
                    Price = 47.21M,
                    Quantity = 20,
                    ProductCategory = new ProductCategory{ Name = "Polo"},
                    ProductCategoryId = 4,
                    Image = new Image{RemoteImageUrl = "new/image.jpg" },
                    ImageId = "123",
                    CreatedByUserId = "ArsenalAdmin",
                    CreatedByUser = new ApplicationUser{UserName = "ArsenalAdmin" },
                },
            };
        }

        [Fact]

        public void GetProducsCount_ShouldReturnCorrectResults()
        {
            var list = this.GetDummyData();

            var mockRepo = new Mock<IDeletableEntityRepository<Product>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Product>())).Callback((Product news) => list.Add(news));

            var service = new ProductService(mockRepo.Object);

            var result = service.GetCount();

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task CreateProduct_WithCorrectData_ShouldSuccessfullyCreate()
        {

            var list = new List<Product>();

            var mockRepo = new Mock<IDeletableEntityRepository<Product>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Product>())).Callback((Product product) => list.Add(product));

            var service = new ProductService(mockRepo.Object);

            IFormFile image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.png");

            var newsCreateInput = new ProductCreateInputModel
            {
                UserId = 6,
                Name = "Arsenal Hat 2020",
                Price = 25M,
                Description = null,
                Quantity = 20,
                Image = image,
            };

            await service.CreateAsync(newsCreateInput, "12", "/img/Arsenal_FC.png");

            Assert.Single(list.Where(x => x.CreatedByUserId == "12"));
        }

        [Fact]
        public async Task CreateProduct_WithWrongImageFormat_ShouldThrowArgumentException()
        {

            var list = new List<Product>();

            var mockRepo = new Mock<IDeletableEntityRepository<Product>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Product>())).Callback((Product product) => list.Add(product));

            var service = new ProductService(mockRepo.Object);

            IFormFile image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.text");

            var newsCreateInput = new ProductCreateInputModel
            {
                UserId = 6,
                Name = "Arsenal Hat 2020",
                Price = 25M,
                Description = null,
                Quantity = 20,
                Image = image,
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(newsCreateInput, "12", "/img/Arsenal_FC.png"));
        }

        [Fact]
        public void ProductPostById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Product>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Product { Id = "1", Name = "test" }).GetAwaiter().GetResult();

            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var productService = new ProductService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestProduct).Assembly);
            var product = productService.GetById<MyTestProduct>("1");

            Assert.Equal("test", product.Name);
        }

        [Fact]
        public void TestGetProducts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Product>(new ApplicationDbContext(options.Options));
            repository.AddAsync
                (new Product
                {
                    Id = "FirstId",
                    Name = "T-Shirt",
                    Price = 78.43M,
                    Quantity = 50,
                    ProductCategory = new ProductCategory { Name = "T-Shirt" },
                    ProductCategoryId = 6,
                    Image = new Image { RemoteImageUrl = "new/image.jpg" },
                    ImageId = "123456",
                    CreatedByUserId = "ArsenalAdmin",
                    CreatedByUser = new ApplicationUser { UserName = "ArsenalAdmin" },
                }).GetAwaiter().GetResult();

            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var productService = new ProductService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestProduct).Assembly);
            var product = productService.GetProducts<MyTestProduct>();

            Assert.Equal(1, product.Count());
        }

        [Fact]
        public void TestGetProducts_WithParameters()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Product>(new ApplicationDbContext(options.Options));
            repository.AddAsync
                (new Product
                {
                    Id = "Firstsss",
                    Name = "T-Shirt",
                    Price = 78.43M,
                    Quantity = 50,
                    ProductCategory = new ProductCategory { Name = "T-Shirt3" },
                    ProductCategoryId = 8,
                    Image = new Image { RemoteImageUrl = "new/image2.jpg" },
                    ImageId = "12342256",
                    CreatedByUserId = "ArsenalAdmin1",
                    CreatedByUser = new ApplicationUser { UserName = "ArsenalAdmin1" },
                }).GetAwaiter().GetResult();

            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var productService = new ProductService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestGetProduct).Assembly);
            var product = productService.GetProducts<MyTestGetProduct>(1, 1);

            Assert.Equal(1, product.Count());
        }

        public class MyTestGetProduct : IMapFrom<Product>
        {
            public string Name { get; set; }

            public decimal Price { get; set; }

            public string ImageId { get; set; }

            public virtual Image Image { get; set; }

            public int Quantity { get; set; }

            public string Description { get; set; }

            public int ProductCategoryId { get; set; }

            public virtual ProductCategory ProductCategory { get; set; }

            public string CreatedByUserId { get; set; }

            public virtual ApplicationUser CreatedByUser { get; set; }
        }

        public class MyTestProduct : IMapFrom<Product>
        {
            public string Name { get; set; }
        }
    }
}
