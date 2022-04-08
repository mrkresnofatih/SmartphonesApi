using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartphonesApi.App.Constants;
using SmartphonesApi.App.Constants.CustomExceptions;

namespace SmartphonesApi.App.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            context.HttpContext.Response.StatusCode = GetStatusCode(exception);
            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new JsonResult(new {});
            
            base.OnException(context);
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