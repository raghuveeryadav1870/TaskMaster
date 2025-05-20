using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASN.Filters
{
    public class FilterAuthCheck : IActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FilterAuthCheck(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                if (string.IsNullOrEmpty(SessionExtensions.GetString(_httpContextAccessor.HttpContext.Session, "UserSession")))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Account" }, { "action", "Login" } });
                }
            }
        }
    }
  
}
