namespace ArsenalFanPage.Services.Data
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task Create(int newsId, string userId, string content, int? parentId = null);

        bool IsInNewsId(int commentId, int newsId);
    }
}
