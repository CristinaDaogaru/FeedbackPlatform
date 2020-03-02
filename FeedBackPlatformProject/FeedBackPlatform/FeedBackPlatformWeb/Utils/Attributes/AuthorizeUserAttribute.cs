using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedBackPlatformWeb.Utils.Attributes
{
    public class AuthorizeUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as Controllers.Base.BaseController;
            if (controller == null || controller.UserContext == null || !controller.UserContext.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { action = "Login", controller = "Account" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}