using CPlatform.Msg.Res;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPP
{
    public class ExceptionHandleAttribute : ExceptionFilterAttribute
    {
        readonly ILogger<ExceptionHandleAttribute> _logger;

        public ExceptionHandleAttribute(ILogger<ExceptionHandleAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            new ResponsePackage().Error(errorMsg: "接口处理异常,请联系管理员");

            var displayUrl = context.HttpContext.Request.GetDisplayUrl();
            var routeValue = context.HttpContext.GetRouteValue("key");

            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendFormat("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            builder.AppendLine();
            builder.AppendFormat("Url：{0}", displayUrl);
            builder.AppendLine();
            builder.AppendFormat("action：{0}", routeValue);
            builder.AppendLine();
            builder.AppendFormat("exception:{0}", context.Exception.Message);
            builder.AppendLine();
            builder.AppendLine();

            _logger.LogError(builder.ToString());
            Debug.Write(builder.ToString());
            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            context.Result = new OkObjectResult(new ResponsePackage().Error(errorMsg: context.Exception.Message));
            base.OnException(context);
        }
    }
}
