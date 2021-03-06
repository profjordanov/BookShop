﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Core.Models.Books;
using BookShop.Core.Models.Books.ServiceModels;
using Optional;

namespace BookShop.Core.Services
{
    public interface IBookService
    {
        Task<Option<BookDetailsServiceModel, Error>> GetById(int id);

        Task<Option<IEnumerable<BookListingServiceModel>, Error>> GetBySearchTerm(string searchTerm);

        Task<Option<BookDetailsServiceModel, Error>> CreateByModel(BookWithCategoriesRequestModel model);

        Task<Option<BookDetailsServiceModel, Error>> UpdateByModelAndId(int id,BookRequestModel model);

        Task<Option<Success, Error>> DeleteById(int bookId);
    }
}