namespace ArsenalFanPage.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ArsenalFanPage.Web.ViewModels.Product;

    public interface IProductService
    {
        Task CreateAsync( ProductCreateInputModel input, string userId, string imagePath);

        IEnumerable<T> GetProducts<T>(int page, int itemsPerPage = 8);

        IEnumerable<T> GetProducts<T>();

        T GetById<T>(string id);

        int GetCount();
    }
}
