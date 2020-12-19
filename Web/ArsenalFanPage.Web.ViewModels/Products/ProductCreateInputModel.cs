namespace ArsenalFanPage.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class ProductCreateInputModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "The name cannot be less then 5 charachters.")]
        [MaxLength(20, ErrorMessage = "The name cannot be more than 20 charachters.")]
        public string Name { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "The description cannot be less then 5 charachters.")]
        [MaxLength(300, ErrorMessage ="The description cannot be more than 300 charachters.")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.PositiveInfinity, ErrorMessage = "The price cannot be less then 4 or more than 1000 charachters.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "The quantity cannot be negative number or more than 1000.")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int ProductCategoryId { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public IEnumerable<ProductDropDownViewModel> Categories { get; set; }
    }
}
