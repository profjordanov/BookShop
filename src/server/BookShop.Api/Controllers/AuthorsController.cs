using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BookShop.Api.Controllers._Base;
using BookShop.Core;
using BookShop.Core.Models.Authors;
using BookShop.Core.Models.Authors.ServiceModels;
using BookShop.Core.Models.Books;
using BookShop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using static BookShop.Api.WebConstants;

namespace BookShop.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors")]
    public class AuthorsController : ApiController
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Creates a new author with first name and last name.
        /// </summary>
        /// <param name="model">The first and last name of the author (mandatory).</param>
        /// <returns>A model of the new author.</returns>
        /// <response code="200">An author was created successfully.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthorRequestModel model)
        {
            return Ok(await _authorService.Create(model));
        }

        /// <summary>
        /// Gets author with id, first name, last name and a list of all his/her book titles.
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        /// <returns>A author model.</returns>
        /// <response code="200">If there is author.</response>
        /// <response code="404">If there isn't such author .</response>
        [HttpGet(WithId)]
        [ProducesResponseType(typeof(AuthorDetailsServiceModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id) =>
            (await _authorService.GetById(id))
                .Match(Ok, Error);

        /// <summary>
        /// Gets books from author by id. Returns all data about the book + category names.
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        /// <returns>Books with categories model.</returns>
        /// <response code="200">If there are books from author.</response>
        /// <response code="404">If there are no books from author.</response>
        [HttpGet(WithId + "/books")]
        [ProducesResponseType(typeof(IEnumerable<BookWithCategoriesServiceModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAuthorBooks(int id) =>
            (await _authorService.BooksByAuthorId(id))
                .Match(Ok, Error);
    }
}