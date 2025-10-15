using EnglishWordsNoteBook.DTO;
using EnglishWordsNoteBook.Exceptions;

namespace EnglishWordsNoteBook.Middlewares
{
    public class GlobalExceptionMiddleware
    {
   
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new GeneralResponse
            {
                Success = false,
                Message = ex.Message,
                Data = null,
                Errors = null
            };

            int statusCode = 500;

            if (ex is HttpStatusCodeException httpEx)
                statusCode = httpEx.StatusCode;

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";


            return context.Response.WriteAsJsonAsync(response);
        }
        

    }
}
