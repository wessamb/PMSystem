using Microsoft.AspNetCore.Mvc.Filters;

namespace PMSystem.ActionFilter
{
    public class CustomResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var Header = context.HttpContext.Request.Headers.ContainsKey("X-Cool-Header");
            if (!Header)
            {
                context.HttpContext.Response.StatusCode = 400;
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new { Message = "X-Cool-Header is required" });
            }
        }
    }
}
