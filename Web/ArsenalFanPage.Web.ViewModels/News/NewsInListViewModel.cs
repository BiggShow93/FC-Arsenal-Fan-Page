﻿namespace ArsenalFanPage.Web.ViewModels.News
{
    using System.Collections.Generic;

    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;
    using AutoMapper;

    public class NewsInListViewModel : IMapFrom<News>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<News, NewsInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(n => n.Image.RemoteImageUrl != null ?
                    n.Image.RemoteImageUrl :
                    "/images/news/" + n.Image.Id + "." + n.Image.Extension));
        }
    }
}
