using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VacancyAggregator.Domain;

namespace VacancyAggregator.WebUI.Middlewares
{
    public class HttpStatusCodeExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpStatusCodeExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<HttpStatusCodeExceptionMiddleware> logger,
          IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    logger.LogWarning(ex, "The response has already started, the http status code middleware will not be executed.");

                    throw;
                }

                context.Response.Clear();
                context.Response.ContentType = "application/json";

                object error;
                int statusCode;

                if (ex is ValidationException validation)
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    error = new
                    {
                        error = validation.ValidationResult.ErrorMessage
                    };
                }
                else if(ex is UserDisplayException userDisplayException)
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    error = new
                    {
                        error = userDisplayException.Message
                    };
                }
                else
                {
                    statusCode = StatusCodes.Status500InternalServerError;
                    var isDevelopment = hostingEnvironment.IsDevelopment();

                    error = new
                    {
                        error = isDevelopment ? ex.Message : "We're Sorry. An unexpected error has occurred. " +
                            "If this continues please contact Tech Support.",
                        stackTrace = isDevelopment ? ex.StackTrace : null
                    };
                }

                context.Response.StatusCode = statusCode;
                var text = JsonSerializer.Serialize(error);

                await context.Response.WriteAsync(text);
            }
        }
    }   
}
