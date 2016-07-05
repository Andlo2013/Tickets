using System;
using System.Collections.Generic;
using System.Web.Mvc;
using _Entidades.specificModels;
using _Helpers;
using _Entidades;
using TicketsMVC.Areas.supportSI.Filters;

namespace TicketsMVC.Areas.supportSI.Controllers
{
    [isAdmin]
    public class si_UsuarioController : Controller
    {
        private usuarioCRUD objUsuario = new usuarioCRUD();
        private Combo objCombo = new Combo();
        // GET: supportSI/si_Usuario
        public ActionResult Index()
        {
            ViewBag.filtro = objCombo._getCombo("filtro_Usuario");
            return View();
        }

        // GET: supportSI/si_Usuario/listarUsuario
        public JsonResult listarUsuario(int jtStartIndex = 0, int jtPageSize = 0,
                                        string campoBuscar = "", string textoBuscar = "")
        {
            try
            {
                List<usuarioCRUD>data= objUsuario._getUsuarios(jtStartIndex, jtPageSize, campoBuscar, textoBuscar);
                return Json(new { Result = "OK", Records = data, TotalRecordCount = objUsuario.pro_getTotalREG }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar recuperar los datos" });
            }
        }

        public JsonResult Create(usuarioCRUD model)
        {
            try
            {
                model.Estado = true;
                model.Horario = model.Horario == null ? "" : model.Horario;
                string password = model.Password == null ? "" : model.Password;
                if (ModelState.IsValid)
                {
                    List<usuarioCRUD> newUsuario=objUsuario._saveUsuario("NEW", model.Nombre, model.Email,
                       password, model.Horario, model.EmpresaRUC, true, true);
                    return Json(new { Result = "OK", Record = newUsuario }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = "ERROR", Message = ModelState._getModelErrors() });
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar guardar el registro" });
            }
        }

        public JsonResult Edit(usuarioCRUD model)
        {
            try {
                model.Horario = model.Horario == null ? "" : model.Horario;
                ModelState.Remove("Password");
                if (ModelState.IsValid)
                {
                    string password = model.Password == null ? "" : model.Password;
                    objUsuario._saveUsuario(model.UUID, model.Nombre, model.Email,
                        password, model.Horario, model.EmpresaRUC, model.Estado, false);
                    return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = "ERROR", Message = ModelState._getModelErrors() });
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar guardar el registro" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
