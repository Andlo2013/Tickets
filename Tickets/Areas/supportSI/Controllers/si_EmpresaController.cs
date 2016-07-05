using System;
using System.Web.Mvc;
using _Entidades;
using System.Collections.Generic;
using _Helpers;
using TicketsMVC.Areas.supportSI.Filters;

namespace TicketsMVC.Areas.supportSI.Controllers
//Portafolio.Areas.admin.Controllers
{
    [isAdmin]
    public class si_EmpresaController : Controller
    {
        private Empresa objEmpresa = new Empresa();
        private Combo objCombo = new Combo();
        // GET: Empresa
        public ActionResult Index()
        {
            ViewBag.filtro = objCombo._getCombo("filtro_Empresa");
            return View();
        }

        // JSON => Detalle de tickets
        [HttpPost]
        public JsonResult listarEmpresa(int jtStartIndex = 0, int jtPageSize = 0,
                                        string campoBuscar="",string textoBuscar="")
        {
            try
            {
                List<Empresa> data = objEmpresa._getEmpresas(jtStartIndex, jtPageSize, campoBuscar, textoBuscar);
                return Json(new { Result = "OK", Records = data, TotalRecordCount = objEmpresa.pro_getTotalREG }, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                return Json(new { Result = "ERROR", Message = "Al intentar recuperar los registros" });
            }
        }

        // POST: Empresa/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(Empresa empresa)
        {
            try {
                empresa.EstReg = true;
                if (ModelState.IsValid)
                {
                    List<Empresa>result=objEmpresa._newEmpresa(empresa.EmpRuc,empresa.EmpNom,
                        empresa.Direccion,empresa.Telefono);
                    return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = ModelState._getModelErrors()});
                }
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar guardar el registro" });
            }
        }

        // POST: Empresa/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Empresa empresa)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objEmpresa._updateEmpresa(empresa.UUID, empresa.EmpRuc, 
                        empresa.EmpNom,empresa.Direccion, empresa.Telefono,empresa.EstReg);
                    return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
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

        public JsonResult getEmpresaRUC(string RUC)
        {
            Empresa empresaData = objEmpresa._getEmpresaRUC(RUC);
            return Json(new { Result = "OK", Records = empresaData },JsonRequestBehavior.AllowGet);
        }

    }
}
