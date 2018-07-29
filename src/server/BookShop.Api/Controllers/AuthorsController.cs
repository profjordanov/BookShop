using System.Threading.Tasks;
using BookShop.Core.Models.Authors;
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
            this._authorService = authorService;
        }

        [HttpGet(WithId)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await this._authorService.Details(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthorRequestModel model)
        {
            var id = await this._authorService.Create(
                model.FirstName.Trim(),
                model.LastName.Trim());

            return Ok(id);
        }
    }
}