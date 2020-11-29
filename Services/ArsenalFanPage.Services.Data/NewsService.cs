namespace ArsenalFanPage.Services.Data
{
    using System.Threading.Tasks;

    using ArsenalFanPage.Data.Common.Repositories;
    using ArsenalFanPage.Data.Models;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsService(IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task CreateAsync(string title, int categoryId, string content, string userId)
        {
            var news = new News()
            {
                CategoryId = categoryId,
                Content = content,
                Title = title,
                CreatedByUserId = userId,
            };

            await this.newsRepository.AddAsync(news);
            await this.newsRepository.SaveChangesAsync();
        }
    }
}
