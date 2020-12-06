namespace ArsenalFanPage.Web.ViewModels.News
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ArsenalFanPage.Data.Models;

    public class NewsCreateInputModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MinLength(10)]
        public string Title { get; set; }

        [Required]
        [MinLength(40)]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Image Image { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
