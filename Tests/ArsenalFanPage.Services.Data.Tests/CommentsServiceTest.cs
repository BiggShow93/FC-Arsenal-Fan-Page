namespace ArsenalFanPage.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ArsenalFanPage.Data.Common.Repositories;
    using ArsenalFanPage.Data.Models;
    using Moq;
    using Xunit;

    public class CommentsServiceTest
    {
        [Fact]
        public async Task CreateComment_WithCorrectData_ShouldSuccessfullyCreate()
        {

            var list = new List<Comment>();

            var mockRepo = new Mock<IDeletableEntityRepository<Comment>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback((Comment news) => list.Add(news));

            var service = new CommentsService(mockRepo.Object);


            await service.Create(1, "some user", "some text..");

            Assert.Single(list.Where(x => x.UserId == "some user"));
        }
    }
}
