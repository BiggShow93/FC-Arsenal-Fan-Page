namespace ArsenalFanPage.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ArsenalFanPage.Web.ViewModels.Category;
    using ArsenalFanPage.Web.ViewModels.News;

    public interface INewsService
    {
        Task CreateAsync(NewsCreateInputModel input, string title, int categoryId, string content, string userId, string imagePath);

        NewsInputModel GetNewsCountByCategory();

        IEnumerable<T> GetNews<T>(int page, int itemsPage = 8);

        IEnumerable<T> GetNews<T>();

        int GetCount();

        T GetById<T>(int id);
    }
}
