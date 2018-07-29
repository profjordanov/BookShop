namespace BookShop.Core.Models.Books
{
    public class BookWithCategoriesRequestModel : BookRequestModel
    {
        public string Categories { get; set; }
    }
}