using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookShop.Data.ModelConstants;

namespace BookShop.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public ICollection<CategoryBook> Books { get; set; } = new HashSet<CategoryBook>();
    }
}