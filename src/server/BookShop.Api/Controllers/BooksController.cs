using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Api.Controllers._Base;
using BookShop.Core.Models.Books;
using BookShop.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Books")]
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BooksController(
            IBookService bookService,
            IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookWithCategoriesRequestModel model)
        {
            var authorExists = await _authorService.Exists(model.AuthorId);
            if (!authorExists)
            {
                return BadRequest("Author does not exist.");
            }

            var id = await _bookService.Create(
                model.Title.Trim(),
                model.Description.Trim(),
                model.Price,
                model.Copies,
                model.Edition,
                model.AgeRestriction,
                model.ReleaseDate,
                model.AuthorId,
                model.Categories);

            return Ok(id);
        }
    }
}