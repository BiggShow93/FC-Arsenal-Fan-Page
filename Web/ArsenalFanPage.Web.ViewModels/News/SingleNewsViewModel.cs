namespace ArsenalFanPage.Web.ViewModels.News
{
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;
    using AutoMapper;

    public class SingleNewsViewModel : IMapFrom<News>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<News, SingleNewsViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(n => n.Image.RemoteImageUrl != null ?
                    n.Image.RemoteImageUrl :
                    "/images/news/" + n.Image.Id + "." + n.Image.Extension));
        }
    }
}

