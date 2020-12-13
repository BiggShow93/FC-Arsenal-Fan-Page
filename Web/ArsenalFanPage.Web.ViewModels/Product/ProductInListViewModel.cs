namespace ArsenalFanPage.Web.ViewModels.Product
{
    using System.Linq;

    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;
    using AutoMapper;

    public class ProductInListViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(n => n.Image.RemoteImageUrl != null ?
                    n.Image.RemoteImageUrl :
                    "/images/products/" + n.Image.Id + "." + n.Image.Extension));
        }
    }
}
