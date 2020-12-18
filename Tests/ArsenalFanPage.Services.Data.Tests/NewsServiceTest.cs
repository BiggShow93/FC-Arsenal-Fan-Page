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
    using ArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class NewsServiceTest
    {
        private List<News> GetDummyData()
        {
            return new List<News>()
            {
                new News
                {
                 Id = 1,
                 CategoryId = 6,
                 Category = new Category{Name = "History" },
                 Content = null,
                 Image = new Image{RemoteImageUrl = "new/image.jpg" },
                 ImageId = "123456",
                 Title = "Arsenal",
                 CreatedByUserId = "asdad",
                 CreatedByUser = new ApplicationUser{UserName = "ArsenalAdmin"},
                },
                new News
                {
                    Id = 2,
                    CategoryId = 1,
                    Category = new Category{Name = "News" },
                    Content = null,
                    Image = new Image{RemoteImageUrl = "new/image.jpg" },
                    ImageId = "123",
                    Title = "Arsenal",
                    CreatedByUserId = "asdad",
                    CreatedByUser = new ApplicationUser{UserName = "ArsenalAdmin"},
                },
            };
        }


        [Fact]
        public void GetNewsCount_WithoutCategory_ShouldReturnCorrectResults()
        {
            var list = this.GetDummyData();

            var mockRepo = new Mock<IDeletableEntityRepository<News>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => list.Add(news));

            var service = new NewsService(mockRepo.Object);

            var result = service.GetCount();

            Assert.Equal(2, result);
        }

        [Fact]
        public void GetNewsCount_WithCategory_ShouldReturnCorrectResults()
        {
            var list = this.GetDummyData();

            var mockRepo = new Mock<IDeletableEntityRepository<News>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => list.Add(news));

            var service = new NewsService(mockRepo.Object);

            var categoty = "History";

            var result = service.GetCount(categoty);

            Assert.Equal(1, result);
        }



        [Fact]
        public void TestGetNewsById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new News {Id = 25, Title = "test" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new NewsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestNews).Assembly);
            var news = postService.GetById<MyTestNews>(25);

            Assert.Equal("test", news.Title);
        }


        [Fact]
        public void TestGetNews()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));
            repository.AddAsync(
                new News
                {
                    Id = 1,
                    Title = "Arsenal",
                    CategoryId = 6,
                    Category = new Category { Name = "History" },
                    Content = null,
                    Image = new Image { RemoteImageUrl = "new/image.jpg" },
                    ImageId = "123456",
                    CreatedByUserId = "asdad",
                    CreatedByUser = new ApplicationUser { UserName = "ArsenalAdmin" },
                }).GetAwaiter().GetResult();

            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new NewsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestNews).Assembly);
            var news = postService.GetNews<MyTestNews>();

            Assert.Equal(1, news.Count());
        }

        [Fact]
        public void TestGetNews_WithPaging()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));
            repository.AddAsync
                (new News
                {
                    Id = 2,
                    Title = "Arsenal",
                    CategoryId = 6,
                    Category = new Category { Name = "History" },
                    Content = null,
                    Image = new Image { RemoteImageUrl = "new/image.jpg" },
                    ImageId = "123456",
                    CreatedByUserId = "asdad",
                    CreatedByUser = new ApplicationUser { UserName = "ArsenalAdmin" },
                }).GetAwaiter().GetResult();

            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var postService = new NewsService(repository);
            AutoMapperConfig.RegisterMappings(typeof(MyTestNews).Assembly);
            var news = postService.GetNews<MyTestNews>(1, "History", 1);

            Assert.Equal(1, news.Count());
        }


        public class MyTestNews : IMapFrom<News>
        {
            public string Title { get; set; }
        }

        [Fact]
        public async Task Create_WithCorrectData_ShouldSuccessfullyCreate()
        {

            var list = new List<News>();

            var mockRepo = new Mock<IDeletableEntityRepository<News>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => list.Add(news));

            var service = new NewsService(mockRepo.Object);

            IFormFile image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.png");

            var newsCreateInput = new NewsCreateInputModel
            {
                UserId = 6,
                Title = "Arsenal",
                Content = null,
                CategoryId = 6,
                Image = image,
            };

            await service.CreateAsync(newsCreateInput, "12", "/img/Arsenal_FC.png");

            Assert.Single(list.Where(x => x.CreatedByUserId == "12"));
        }

        [Fact]
        public async Task Create_WithWrongImageFormat_ShouldThrowException()
        {

            var list = new List<News>();

            var mockRepo = new Mock<IDeletableEntityRepository<News>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<News>())).Callback((News news) => list.Add(news));

            var service = new NewsService(mockRepo.Object);

            IFormFile image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");


            var newsCreateInput = new NewsCreateInputModel
            {
                UserId = 6,
                Title = "Arsenal",
                Content = null,
                CategoryId = 6,
                Image = image,
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(newsCreateInput, "12", "/img/Arsenal_FC.png"));

        }
    }
}
