namespace PMSystem.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context) {
            Console.WriteLine("Custom Middleware: Request Processing Started");
            await _next(context);
            Console.WriteLine("Custom Middleware: Response Sent");
        }
    }
}
