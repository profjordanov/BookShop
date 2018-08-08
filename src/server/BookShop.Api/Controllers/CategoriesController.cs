using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BookShop.Api.Controllers._Base;
using BookShop.Core;
using BookShop.Core.Models.Categories;
using BookShop.Core.Services;
using Microsoft.AspNetCore.Mvc;
using static BookShop.Api.WebConstants;

namespace BookShop.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Categories")]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="model">The name of the category.</param>
        /// <returns>A model of the new category.</returns>
        /// <response code="201">A category was created successfully.</response>
        /// <response code="400">Already existing category.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryServiceModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CategoryRequestModel model) =>
            (await _categoryService.CreateByName(model.Name.Trim()))
                .Match(c => CreatedAtAction(nameof(Post), c), Error);

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A collection of categories.</returns>
        /// <response code="200">If there are categories.</response>
        /// <response code="404">If no category exists.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryServiceModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get() =>
            (await _categoryService.All())
            .Match(Ok, Error);

        /// <summary>
        /// Gets a category.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>A category model.</returns>
        /// <response code="200">If there is category.</response>
        /// <response code="404">If there isn't such category .</response>
        [HttpGet(WithId)]
        [ProducesResponseType(typeof(CategoryServiceModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id) =>
            (await _categoryService.GetById(id))
            .Match(Ok, Error);

        /// <summary>
        /// Edits a category name.
        /// </summary>
        /// <param name="model">Id and new name of the category.</param>
        /// <returns>A model of the new category.</returns>
        /// <response code="200">A category was updated successfully.</response>
        /// <response code="400">Already existing category name or invalid id.</response>
        [HttpPut]
        [ProducesResponseType(typeof(CategoryServiceModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put([FromBody] CategoryServiceModel model) =>
            (await _categoryService.UpdateByModel(model))
            .Match(Ok, Error);

        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>Success/Error message.</returns>
        /// <response code="200">If successfully deleted.</response>
        /// <response code="404">If there isn't such category.</response>
        [HttpDelete]
        [ProducesResponseType(typeof(CategoryServiceModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id) =>
            (await _categoryService.DeleteById(id))
            .Match(Success, Error);
    }
}