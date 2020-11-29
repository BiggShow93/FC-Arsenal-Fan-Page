namespace ArsenalFanPage.Services.Data
{
    using System.Threading.Tasks;

    public interface INewsService
    {
        Task CreateAsync(string title, int categoryId, string content, string userId);
    }
}
