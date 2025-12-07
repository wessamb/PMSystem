namespace PMSystem.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
       public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try {
                _logger.LogInformation("Handling request: {Method} {Url} ", context.Request.Method, context.Request.Path);
            await _next(context);


            }
            finally
            {
                stopwatch.Stop();

                // سجل الاستجابة الصادرة مع مدة تنفيذ الطلب
                _logger.LogInformation(
                    "Finished request {Path} with status code {StatusCode} in {Duration}ms",
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }

        }
    }
}
