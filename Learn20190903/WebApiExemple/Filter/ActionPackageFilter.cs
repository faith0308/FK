using CPlatform.Msg.Res;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiExemple.Filter
{
    /// <summary>
    /// 动作过滤器
    /// </summary>
    public class ActionPackageFilter : IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result != null)
            {
                context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                if (context.Result.GetType() == typeof(ObjectResult))
                {
                    var objectResult = context.Result as ObjectResult;
                    if (objectResult == null) throw new ArgumentNullException(nameof(objectResult));
                    //等Controller的Action方法执行完后，通过更改ActionExecutedContext类的Result属性，来替换Action方法返回的Json对象
                    context.Result = new OkObjectResult(new DynamicResponsePackage().Success(result: objectResult.Value));
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
