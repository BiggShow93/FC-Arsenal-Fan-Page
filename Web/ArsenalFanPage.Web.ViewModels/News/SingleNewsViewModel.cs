namespace ArsenalFanPage.Web.ViewModels.News
{
    using System;
    using System.Collections.Generic;

    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;
    using AutoMapper;
    using Ganss.XSS;

    public class SingleNewsViewModel : IMapFrom<News>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public IEnumerable<NewsCommentViewModel> Comments { get; set; }

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
