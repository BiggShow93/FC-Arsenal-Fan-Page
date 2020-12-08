﻿namespace ArsenalFanPage.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ArsenalFanPage.Data.Common.Repositories;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;
    using ArsenalFanPage.Web.ViewModels.Category;
    using ArsenalFanPage.Web.ViewModels.News;

    public class NewsService : INewsService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "jpeg", "png", "gif" };
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsService(
            IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public NewsInputModel GetNewsCountByCategory()
        {
            var data = new NewsInputModel
            {
                HistoryCount = this.newsRepository.All().Select(x => x.Category.Name == "History").Count(),
            };

            return data;
        }

        public async Task CreateAsync(NewsCreateInputModel input, string title, int categoryId, string content, string userId, string imagePath)
        {
            var news = new News()
            {
                CategoryId = categoryId,
                Content = content,
                Title = title,
                CreatedByUserId = userId,
            };

            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            Directory.CreateDirectory($"{imagePath}/news/");

            var dbImage = new Image
            {
                News = news,
                Extension = extension,
            };

            news.Image = dbImage;
            //news.ImageId = dbImage.Id;

            var physicalPath = $"{imagePath}/news/{dbImage.Id}.{extension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);

            await this.newsRepository.AddAsync(news);
            await this.newsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetNews<T>(int page, int itemsPerPage = 8)
        {
            var news = this.newsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return news;
        }

        public int GetCount()
        {
            return this.newsRepository.All().Count();
        }
    }
}
