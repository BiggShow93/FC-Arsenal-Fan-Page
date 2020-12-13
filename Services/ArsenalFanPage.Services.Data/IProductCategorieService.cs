namespace ArsenalFanPage.Services.Data
{
    using System.Collections.Generic;

    public interface IProductCategorieService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
