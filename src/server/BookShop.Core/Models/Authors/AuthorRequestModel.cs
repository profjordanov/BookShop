using System.ComponentModel.DataAnnotations;
using static BookShop.Data.ModelConstants;

namespace BookShop.Core.Models.Authors
{
    public class AuthorRequestModel
    {
        [Required]
        [MaxLength(AuthorNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(AuthorNameMaxLength)]
        public string LastName { get; set; }
    }
}