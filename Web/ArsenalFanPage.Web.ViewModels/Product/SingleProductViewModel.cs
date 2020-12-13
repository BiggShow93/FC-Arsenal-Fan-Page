namespace ArsenalFanPage.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using System.Linq;

    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;
    using AutoMapper;

    public class SingleProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, SingleProductViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(n => n.Image.RemoteImageUrl != null ?
                    n.Image.RemoteImageUrl :
                    "/images/products/" + n.Image.Id + "." + n.Image.Extension));
        }
    }
}
