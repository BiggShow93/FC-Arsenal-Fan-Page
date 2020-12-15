namespace ArsenalFanPage.Web.ViewModels.Product
{
    using System.Collections.Generic;

    public class ProductListViewModel : PagingViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }
    }
}
