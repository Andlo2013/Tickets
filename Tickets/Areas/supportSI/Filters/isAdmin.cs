using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _Helpers;
using System.Web.Routing;

namespace TicketsMVC.Areas.supportSI.Filters
{
    public class isAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            if (!_SessionHelper.ExistUserInSession() || _SessionHelper.GetUserType()!=1)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Authenticate",
                    action = "Login",
                    area=""
                }));
            }
        }
    }

}