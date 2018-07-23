using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors")]
    public class AuthorsController : ApiController
    {
    }
}