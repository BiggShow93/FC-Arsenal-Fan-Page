namespace ArsenalFanPage.Web.ViewModels.Product
{
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;

    public class ProductDropDownViewModel : IMapFrom<ProductCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
