using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace KATCinema.Utils
{
    public class CustomAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public string Role { get; set; }

        public CustomAuthorizationFilterAttribute()
        {
            
        }

        public CustomAuthorizationFilterAttribute(string roles)
        {
            Role = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!IsAuthorized(context.HttpContext.User))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Account" },  
                    { "action", "Login" }
                });
                return;
            }

            if (Role != null)
            {
                if (!IsInRole(context.HttpContext.User))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "controller", "Home" },
                        { "action", "Index" }
                    });
                }
            }
        }

        private bool IsAuthorized(ClaimsPrincipal user) => user.Identity.IsAuthenticated;

        private bool IsInRole(ClaimsPrincipal user) => user.IsInRole(Role);
    }
}