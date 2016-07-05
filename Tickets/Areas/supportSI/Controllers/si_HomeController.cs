using System;
using System.Web.Mvc;
using TicketsMVC.Areas.supportSI.Filters;

namespace TicketsMVC.Areas.supportSI.Controllers
{
    [isAdmin]
    public class si_HomeController : Controller
    {
        // GET: supportSI/si_Home
        public ActionResult Index()
        {
            return View();
        }
    }
}