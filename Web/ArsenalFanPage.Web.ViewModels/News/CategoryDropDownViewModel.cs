using ArsenalFanPage.Data.Models;
using ArsenalFanPage.Services.Mapping;

namespace ArsenalFanPage.Web.ViewModels.News
{
    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
