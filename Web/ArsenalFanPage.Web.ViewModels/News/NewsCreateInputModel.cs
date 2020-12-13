namespace ArsenalFanPage.Web.ViewModels.News
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class NewsCreateInputModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [MinLength(40)]
        [MaxLength(5000)]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }
    }
}
