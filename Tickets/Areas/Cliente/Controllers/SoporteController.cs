using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using _Entidades;
using _Entidades.specificModels;
using Tickets.Areas.Cliente.Filters;

namespace TicketsMVC.Areas.Cliente.Controllers
{
    [isClient]
    public class SoporteController : Controller
    {

        private Combo objCombo = new Combo();
        private contratoCRUD objContrato = new contratoCRUD();
        private TicketsModel objTicket = new TicketsModel();
        private TicketsDETModel objAnswer = new TicketsDETModel();
        private string m_errors = "";

        //Métodos para los tickets
        #region Tickets

        // GET: Index ticket
        public ActionResult Index()
        {
            try
            {
                ViewBag.cmbEstado = objCombo._getCombo("ticket_estado");
                _infoContrato();
                return View();
            }
            catch
            {
                return null;
            }
        }

        // JSON => Detalle de tickets
        [HttpPost]
        public JsonResult listarTicket(int ticketnumero = 0, int ticketestado = 0,
                                        int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                List<TicketsModel> data = objTicket._getTicketsCLI(ticketnumero, ticketestado, jtStartIndex, jtPageSize);
                return Json(new { Result = "OK", Records = data, TotalRecordCount = objTicket.pro_getTotalREG }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                //NO USAMOS EL EX POR SEGURIDAD
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar recuperar los tickets" });
            }
        }

        // POST => Guarda ticket
        [HttpPost]
        public JsonResult nuevoTicket(string teamViewer, string Pregunta)
        {
            try
            {
                m_errors = "";
                if (_Validar(teamViewer, Pregunta))
                {
                    List<TicketsModel> newTicket = objTicket._saveTicketCLI(teamViewer,Pregunta);
                    if (newTicket != null && newTicket.Count == 1)
                    {
                        return Json(new { Result = "OK", Record = newTicket[0] }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { Result = "ERROR", m_errors });
            }
            catch
            {
                //NO USAMOS EL EX POR SEGURIDAD
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar generar el ticket" });
            }
        }

        #endregion

        //Métodos para las respuestas
        #region Answer

        // POST => Guarda respuesta
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult nuevoAnswer(string id, string teamViewer, string Mensaje, string observacion)
        {
            try
            {
                m_errors = "";
                if (_Validar(teamViewer, Mensaje))
                {
                    List<TicketsDETModel> newAnswer = objAnswer._saveAnswerCLI(id, teamViewer, Mensaje,observacion);
                    if(newAnswer != null && newAnswer.Count == 1)
                    {
                        return Json(new { Result = "OK", Record = newAnswer[0]}, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { Result = "ERROR", Message=m_errors});
            }
            catch
            {
                //NO USAMOS EL EX POR SEGURIDAD
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar guardar la respuesta" });
            }
        }

        // JSON => Detalle de respuestas
        public JsonResult listarAnswer(string id,int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                List<TicketsDETModel> data = objAnswer._getAnswerCLI(id, jtStartIndex, jtPageSize);
                return Json(new { Result = "OK", Records = data, TotalRecordCount = objAnswer.pro_getTotalREG }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                //NO USAMOS EL EX POR SEGURIDAD
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar recuperar el detalle del ticket" });
            }
        }

        // GET => Detalle respuesta
        public ActionResult verAnswer(string id)
        {
            TicketsDetalle showTicketDET = objAnswer._getAnswerUUID(id);
            if (showTicketDET == null)
            {
                return HttpNotFound();
            }
            return View(showTicketDET);
        }

        //[HttpPost]
        public ActionResult UploadFile(string id, string img)
        {
            bool isUploaded = false;
            string message = "File upload failed";
            try
            {
                HttpPostedFileBase myFile = Request.Files["files[]"];
                if (myFile != null && myFile.ContentLength != 0)
                {
                    string pathForSaving = Server.MapPath("~/Uploads/" + id);
                    objAnswer._UploadFile(id, img, pathForSaving, myFile);
                    isUploaded = true;
                    message = "File uploaded successfully!";
                }
            }
            catch (Exception ex)
            {
                message = string.Format("File upload failed: {0}", ex.Message);
            }
            return Json(new { isUploaded = isUploaded, message = message }, "text/html");
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing){}
            base.Dispose(disposing);
        }

        #region MétodosComplementarios

        private void _infoContrato()
        {
            DataRow infoContrato=objContrato._getInfoContrato();
            if (infoContrato != null)
            {
                ViewBag.Empresa = infoContrato["Empresa"].ToString().Trim();
                ViewBag.Plan = infoContrato["Plan"].ToString().Trim();
                ViewBag.Inicia = infoContrato["Inicia"].ToString().Trim();
                ViewBag.Termina = infoContrato["Termina"].ToString().Trim();
                ViewBag.Minutos = infoContrato["Minutos"].ToString().Trim();
                ViewBag.MinutosUTI = infoContrato["MinutosUTI"].ToString().Trim();
            }
        }

        private bool _Validar(string teamViewer,string Mensaje)
        {
            bool isValid = true;
            if (teamViewer.Trim() == "")
            {
                m_errors = "El campo ID TeamViewer es obligatorio";
                isValid = false;
            }
            else if (Mensaje.Trim() == "")
            {
                m_errors = "El campo mensaje es obligatorio";
                isValid = false;
            }
            return isValid;
        }

        #endregion

    }

}
