namespace BookShop.Core.Models.Books.ServiceModels
{
    public class BookDetailsServiceModel : BookWithCategoriesServiceModel
    {
        public string Author { get; set; }
    }
}