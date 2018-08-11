using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BookShop.Api.Controllers._Base;
using BookShop.Core;
using BookShop.Core.Models.Books;
using BookShop.Core.Models.Books.ServiceModels;
using BookShop.Core.Models.Categories;
using BookShop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using static BookShop.Api.WebConstants;

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

        /// <summary>
        /// Adds a new book with title, description, price, copies, edition, age restriction,
        /// release date and a string with space-separated category names.
        /// </summary>
        /// <param name="model">Book with categories.</param>
        /// <returns>A model of the new book.</returns>
        /// <response code="201">A book was created successfully.</response>
        /// <response code="404">Author doesn't exists.</response>
        [HttpPost]
        [ProducesResponseType(typeof(BookDetailsServiceModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Post([FromBody] BookWithCategoriesRequestModel model) =>
            (await _bookService.CreateByModel(model))
            .Match(b => CreatedAtAction(nameof(Post), b), Error);

        /// <summary>
        /// Gets data about a book by id.
        /// Returns all data about the book + category names + author name and id.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>A book model.</returns>
        /// <response code="200">If there is book.</response>
        /// <response code="404">If there isn't such book.</response>
        [HttpGet(WithId)]
        [ProducesResponseType(typeof(BookDetailsServiceModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id) =>
            (await _bookService.GetById(id))
                .Match(Ok, Error);

        /// <summary>
        /// Gets top 10 books which contain the given substring,
        /// sorted by title (ascending). Returns only the title and id of the books.
        /// </summary>
        /// <param name="searchTerm">A word that can be found in the title of the books.</param>
        /// <returns>Collection of book models.</returns>
        /// <response code="200">If there is book which contains the given substring.</response>
        /// <response code="404">If there isn't such book.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookListingServiceModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery] string searchTerm = "") =>
            (await _bookService.GetBySearchTerm(searchTerm))
                .Match(Ok, Error);

        /// <summary>
        /// Edits the book. Receives book title, description,
        /// price, copies, edition, age restriction, release date and author id.
        /// </summary>
        /// <param name="id">ID of the book</param>
        /// <param name="model">Data for book title, description,
        /// price, copies, edition, age restriction, release date and author id..</param>
        /// <returns>A model of the new book.</returns>
        /// <response code="200">A book was updated successfully.</response>
        /// <response code="400">Invalid book id or author id.</response>
        [HttpPut(WithId)]
        [ProducesResponseType(typeof(BookDetailsServiceModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id,[FromBody] BookRequestModel model) =>
            (await _bookService.UpdateByModelAndId(id,model))
                .Match(Ok, Error);

        /// <summary>
        /// Deletes the book.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>Success/Error message.</returns>
        /// <response code="200">If successfully deleted.</response>
        /// <response code="404">If there isn't such book.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(Success), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id) =>
            (await _bookService.DeleteById(id))
                .Match(Success, Error);
    }
}