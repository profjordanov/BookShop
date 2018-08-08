using BookShop.Core;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers._Base
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        protected IActionResult Error(Error error) =>
            new BadRequestObjectResult(error);

        protected IActionResult Success(Success success) =>
            new OkObjectResult(success);
    }
}
