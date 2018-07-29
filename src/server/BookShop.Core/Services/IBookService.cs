using System;
using System.Threading.Tasks;

namespace BookShop.Core.Services
{
    public interface IBookService
    {
        Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime? releaseDate,
            int authorId,
            string categories);
    }
}