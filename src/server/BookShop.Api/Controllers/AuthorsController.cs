﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Core.Models.Authors;
using BookShop.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors")]
    public class AuthorsController : ApiController
    {
        private readonly IAuthorService authorService;
        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthorRequestModel model)
        {
            var id = await this.authorService.Create(
                model.FirstName.Trim(),
                model.LastName.Trim());

            return Ok(id);
        }
    }
}