using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BookShop.Data.ModelConstants;

namespace BookShop.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(AuthorNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(AuthorNameMaxLength)]
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}