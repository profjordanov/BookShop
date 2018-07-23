using System.Linq;
using BookShop.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.Api.Filters
{
    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context
                    .ModelState
                    .Values
                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                context.Result = new BadRequestObjectResult(new Error(errors));
            }
        }
    }
}
