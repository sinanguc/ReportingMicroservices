using Common.Dto.Shared;
using Common.Helpers.ErrorHandling.CustomErrors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Middleware.ExceptionHandler
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            GenericResult result = new GenericResult();
            result.Message = ex.Message;
            result.StatusCode = HttpStatusCode.InternalServerError.GetHashCode(); // eğer hata tipleri ile uyuşmazsa

            switch (ex)
            {
                case LoginIncorrectException:
                case TokenException:
                    result.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
                    break;
                case RecordExistException:
                    result.StatusCode = HttpStatusCode.NotFound.GetHashCode();
                    break;
                case ValidationException:
                case ArgumentNullException:
                case RecordNotFoundException:
                    result.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                    break;
                case DatabaseException:
                    result.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                    break;
                default:
                    result.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                    break;
            }

            context.Response.StatusCode = result.StatusCode;
            context.Response.ContentType = "application/problem+json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            await context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
