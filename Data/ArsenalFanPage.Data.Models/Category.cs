﻿namespace ArsenalFanPage.Data.Models
{
    using System.Collections.Generic;

    using ArsenalFanPage.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.News = new HashSet<News>();
        }

        public string Name { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
