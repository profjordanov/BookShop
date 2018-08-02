using System.ComponentModel.DataAnnotations;
using static BookShop.Data.ModelConstants;

namespace BookShop.Core.Models.Categories
{
    public class CategoryRequestModel
    {
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}