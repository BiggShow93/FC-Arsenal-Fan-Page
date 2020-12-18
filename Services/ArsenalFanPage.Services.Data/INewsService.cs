namespace ArsenalFanPage.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ArsenalFanPage.Web.ViewModels.Category;
    using ArsenalFanPage.Web.ViewModels.News;

    public interface INewsService
    {
        Task CreateAsync(NewsCreateInputModel input, string userId, string imagePath);

        IEnumerable<T> GetNews<T>(int page, string category, int itemsPage);

        IEnumerable<T> GetNews<T>();

        public int GetCount(string categoryName = null);

        T GetById<T>(int id);
    }
}
