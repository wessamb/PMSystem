using Microsoft.AspNetCore.Mvc.Filters;

namespace PMSystem.ActionFilter
{
    public class CustomAuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }
    }
}
