using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAppMVCBatch9
{
    public class SetSessionGlobally : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var value = context.HttpContext.Session.GetString("username");
            if (value == null)
            {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary {
                    {"Controller","Curd" },
                    {"Action","Login" }
                    });
            }

        }
    }
}
