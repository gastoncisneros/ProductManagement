using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product_Management_Core.Exceptions;
using Product_Management_Shared;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Product_Management.Middlewares
{
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            object error = new ErrorBase { Descripcion = "Ocurrió un error en la ejecución del request." };

            if (exception is ProductManagementException)
            {
                error = (exception as ProductManagementException).DataObject;
                code = HttpStatusCode.BadRequest;
            }

            var extendedError = BuildExceptionMessage(exception);
            _logger.LogDebug(extendedError);

            var result = JsonConvert.SerializeObject(error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }

        private string BuildExceptionMessage(Exception e)
        {
            string message = string.Empty;

            try { message += $"\r\n\t{e.GetType().ToString()}: {e.Message}"; }
            catch { }
            if (e.InnerException != null)
                message += BuildExceptionMessage(e.InnerException);

            return message;
        }
    }
}