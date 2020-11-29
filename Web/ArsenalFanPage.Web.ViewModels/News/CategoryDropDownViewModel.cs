namespace ArsenalFanPage.Web.ViewModels.News
{
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
