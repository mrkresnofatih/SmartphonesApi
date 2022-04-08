using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SmartphonesApi.App.Constants;
using SmartphonesApi.App.Constants.CustomExceptions;

namespace SmartphonesApi.App.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            
            _logger.LogError(exception.GetType().FullName + "\n\t" + exception.StackTrace);

            context.HttpContext.Response.StatusCode = GetStatusCode(exception);
            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new JsonResult(new {});
        }

        private static int GetStatusCode(Exception exception)
        {
            string statusCode;
            switch (exception)
            {
                case RecordNotFoundException:
                    statusCode = CustomErrorCodes.RecordNotFound;
                    break;
                default:
                    statusCode = CustomErrorCodes.Unhandled;
                    break;
            }

            return int.Parse(statusCode);
        }
    }
}