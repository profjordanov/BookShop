using BookShop.Core;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        protected IActionResult Error(Error error) =>
            new BadRequestObjectResult(error);
    }
}
