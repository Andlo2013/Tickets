using System.Web.Mvc;

namespace TicketsMVC.Areas.supportSI
{
    public class supportSIAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "supportSI";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "supportSI_default",
                "supportSI/{controller}/{action}/{id}",
                new { controller = "si_Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}