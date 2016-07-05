using System;
using System.Collections.Generic;
using System.Web.Mvc;
using _Entidades;
using _Entidades.specificModels;
using _Helpers;
using TicketsMVC.Areas.supportSI.Filters;

namespace TicketsMVC.Areas.supportSI.Controllers
{
    [isAdmin]
    public class si_ContratoController : Controller
    {
        private contratoCRUD objContrato = new contratoCRUD();
        private Combo objCombo = new Combo();
        private string m_errors = "";
        // GET: si_Contrato
        public ActionResult Index()
        {
            ViewBag.filtro = objCombo._getCombo("filtro_Contrato");
            return View();
        }

        // JSON => Detalle de tickets
        [HttpPost]
        public JsonResult listarContrato(int jtStartIndex = 0, int jtPageSize = 0,
                                        string campoBuscar = "", string textoBuscar = "")
        {
            try
            {
                List<contratoCRUD> data = objContrato._getContratos(jtStartIndex, jtPageSize, campoBuscar, textoBuscar);
                return Json(new { Result = "OK", Records = data,TotalRecordCount = objContrato.pro_getTotalREG }, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar recuperar los datos" });
            }
        }

        // POST: si_Contrato/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(contratoCRUD contrato)
        {
            try
            {
                m_errors = "";
                //configuraciones propias de un CONTRATO NUEVO
                contrato.Estado = true;
                contrato.UUID = "NEW";
                if (_Validate(contrato))
                {
                    List<contratoCRUD> newContrato = objContrato._saveContrato(contrato, true);
                    return Json(new { Result = "OK", Record = newContrato }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = "ERROR", Message = ModelState._getModelErrors() });
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar guardar el registro" }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: si_Contrato/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(contratoCRUD contrato)
        {
            try
            {
                m_errors = "";
                if (_Validate(contrato))
                {
                    List<contratoCRUD> updateContrato = objContrato._saveContrato(contrato, false);
                    return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = "ERROR", Message = m_errors });

            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar guadar el registro" });
            }
        }

        // GET: si_Contrato/Details/5
        public ActionResult Details(int id)
        {
            Contrato contrato = null;//objContrato._getContratoId(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }

        private bool _Validate(contratoCRUD contrato)
        {
            if (contrato.Inicia > contrato.Termina)
            {
                DateTime aux = contrato.Inicia;
                contrato.Inicia = contrato.Termina;
                contrato.Termina = aux;
            }
            bool isValid = true;
            if (contrato.PlanId <= 0)
            {
                m_errors = "No se ha definido el tipo de Plan";
                isValid = false;
            }
            if (contrato.Termina.Subtract(contrato.Inicia).TotalDays < 30)
            {
                m_errors = "El periodo de vigencia del contrato debe ser al menos de 30 días";
                isValid = false;
            }
            return isValid;
        }

    }
}
