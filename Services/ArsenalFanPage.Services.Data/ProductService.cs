namespace ArsenalFanPage.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ArsenalFanPage.Data.Common.Repositories;
    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;
    using ArsenalFanPage.Web.ViewModels.Product;

    public class ProductService : IProductService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "jpeg", "png", "gif" };
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task CreateAsync(
            ProductCreateInputModel input, string userId, string name, string description, decimal price, int quantity, int productCategoryId, string imagePath)
        {
            var product = new Product
            {
               CreatedByUserId = userId,
               Name = name,
               Description = description,
               Price = price,
               Quantity = quantity,
               ProductCategoryId = productCategoryId,
            };

            Directory.CreateDirectory($"{imagePath}/products/");

            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    Extension = extension,
                };

                product.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/products/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetProducts<T>(int page, int itemsPerPage = 8)
        {
            var products = this.productRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return products;
        }

        public IEnumerable<T> GetProducts<T>()
        {
            var products = this.productRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .To<T>()
                .ToList();

            return products;
        }

        public T GetById<T>(string id)
        {
            var products = this.productRepository.AllAsNoTracking()
                  .Where(x => x.Id == id)
                  .To<T>()
                  .FirstOrDefault();

            return products;
        }

        public int GetCount()
        {
            return this.productRepository.All().Count();
        }
    }
}
