using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RRJConverter.Middlewares
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        /// <summary>
        /// Логгер приходящий из конструктора через DI
        /// </summary>
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        
        /// <summary>
        /// Конструктор, принимает логгер в качестве параметра
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        /// <summary>
        /// Обрабатывает запрос клиента
        /// </summary>
        /// <param name="context">Контекст запроса</param>
        /// <param name="next">Делегат следующего middleware в конвеере</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        /// <summary>
        /// Обрабатывает исключение, вызванное в ходе работы InvokeAsync
        /// </summary>
        /// <param name="httpContext">Контекст запроса</param>
        /// <param name="exception">Объект исключения</param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                error = exception.Message
            };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
