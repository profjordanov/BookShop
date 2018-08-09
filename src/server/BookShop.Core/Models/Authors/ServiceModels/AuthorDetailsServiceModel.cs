using System.Collections.Generic;

namespace BookShop.Core.Models.Authors.ServiceModels
{
    public class AuthorDetailsServiceModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<string> Books { get; set; }
    }
}