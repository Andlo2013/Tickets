using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _Entidades;
using _Entidades.specificModels;
using _Helpers;
using TicketsMVC.Areas.supportSI.Filters;

namespace TicketsMVC.Areas.supportSI.Controllers
{
    [isAdmin]
    public class si_PlanController : Controller
    {
        Plan objPlan = new Plan();
        

        // GET: Plan
        public ActionResult Index()
        {
            return View();
        }

        // JSON => Detalle de tickets
        [HttpPost]
        public JsonResult listarPlan(int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                List<Plan> data = objPlan._getPlans();
                return Json(new { Result = "OK", Records = data }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "Al intentar cargar los datos" });
            }
        }

        // POST: Plan/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(Plan plan)
        {
            try
            {
                plan.EstReg = true;
                plan.UUID = "NEW";
                if (ModelState.IsValid)
                {
                    List<Plan> data=objPlan._savePlan(plan,true);
                    return Json(new { Result = "OK",Record=data });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = ModelState._getModelErrors() });
                }
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar guardar el registro" });
            }
        }

        // POST: Plan/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Plan plan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objPlan._savePlan(plan,false);
                    return Json(new { Result = "OK" });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = ModelState._getModelErrors() });
                }
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar guardar el registro" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing){}
            base.Dispose(disposing);
        }

        public JsonResult getPlanCMB()
        {
            try
            {
                List<planCMB> data = objPlan._getPlansCMB();
                return Json(new { Result = "OK", Options = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult getPlanId(int idPlan)
        {
            try
            {
                Plan infoPlan = objPlan._getPlanId(idPlan);
                return Json(new { Result = "OK", Records = infoPlan }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
