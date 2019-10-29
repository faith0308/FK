using CPlatform.Msg.Res;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPP
{
    public class ActionPackageFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result != null)
            {
                context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                if (context.Result.GetType() == typeof(ObjectResult))
                {
                    var objectResult = context.Result as ObjectResult;
                    if (objectResult == null) throw new ArgumentNullException(nameof(objectResult));
                    context.Result = new OkObjectResult(new DynamicResponsePackage().Success(result: objectResult.Value));
                }

            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}