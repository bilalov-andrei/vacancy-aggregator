using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace VacancyAggregator.WebUI.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} {Query} {QueryString} " +
                                               "responded {StatusCode} in {Elapsed:0.0000} ms";
        private const string ExceptionMessageTemplate = "---------------HTTP {RequestHeaders} {RequestHost} {RequestProtocol} {RequestForm}" +
                                                        " {RequestMethod} {RequestPath} {Query} {QueryString} " +
                                                        "responded {StatusCode} in {Elapsed:0.0000} ms";

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger<LoggingMiddleware> logger)
        {
            var start = Stopwatch.GetTimestamp();
            try
            {
                await _next(httpContext);

                var elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());

                var statusCode = httpContext.Response?.StatusCode;
                var level = statusCode >= 500 ? LogLevel.Error : LogLevel.Information;

                logger.Log(level, MessageTemplate,
                  httpContext.Request.Method, httpContext.Request.Path,
                  httpContext.Request.Query, httpContext.Request.QueryString,
                  statusCode, elapsedMs
                );
            }
            // Исключение не будет поймано, поскольку `LogException()` вернёт false.
            // Такой подход нужен чтобы получить контекстную информацию об эксепшене
            catch (Exception ex) when (
              LogException(httpContext, logger, GetElapsedMilliseconds(start, Stopwatch.GetTimestamp()), ex)
              )
            {
            }
        }

        private static bool LogException(HttpContext httpContext, ILogger logger, double elapsedMs, Exception ex)
        {
            var request = httpContext.Request;

            string requestForm = string.Empty;
            if (request.HasFormContentType)
            {
                requestForm = string.Join(Environment.NewLine, request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));
            }

            logger.LogError(ex, ExceptionMessageTemplate, request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                request.Host, request.Protocol, requestForm, request.Method, request.Path,
              request.Query, request.QueryString, 500, elapsedMs);

            return false;
        }

        private static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }
    }
}
