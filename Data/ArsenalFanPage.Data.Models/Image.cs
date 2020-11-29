namespace ArsenalFanPage.Data.Models
{
    using System;

    using ArsenalFanPage.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int NewsId { get; set; }

        public virtual News News { get; set; }

        public string Extension { get; set; }
    }
}
