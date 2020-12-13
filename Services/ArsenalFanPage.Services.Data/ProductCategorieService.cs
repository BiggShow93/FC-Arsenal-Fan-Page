namespace ArsenalFanPage.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ArsenalFanPage.Data.Common.Repositories;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;

    public class ProductCategorieService : IProductCategorieService
    {
        private readonly IDeletableEntityRepository<ProductCategory> productCategoriesRepository;

        public ProductCategorieService(
            IDeletableEntityRepository<ProductCategory> productCategoriesRepository)
        {
            this.productCategoriesRepository = productCategoriesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ProductCategory> query =
                this.productCategoriesRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var category = this.productCategoriesRepository.All()
                .Where(x => x.Name.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>().FirstOrDefault();
            return category;
        }
    }
}
