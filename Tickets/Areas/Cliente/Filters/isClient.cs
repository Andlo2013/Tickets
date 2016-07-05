using _Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tickets.Areas.Cliente.Filters
{
    public class isClient : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!_SessionHelper.ExistUserInSession() || _SessionHelper.GetUserType() != 2)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Authenticate",
                    action = "Login",
                    area = ""
                }));
            }
        }
    }
}