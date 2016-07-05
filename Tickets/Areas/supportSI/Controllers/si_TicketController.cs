using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _Entidades;
using _Entidades.specificModels;
using TicketsMVC.Areas.supportSI.Filters;

namespace TicketsMVC.Areas.supportSI.Controllers
{
    [isAdmin]
    public class si_TicketController : Controller
    {
        private Combo objCombo = new Combo();
        private TicketsModel objTicket = new TicketsModel();
        private TicketsDETModel objAnswer = new TicketsDETModel();
        private Tecnico objTecnico = new Tecnico();
        private TicketsCategoria objCategoria = new TicketsCategoria();
        private string m_errors = "";
        

        // GET: supportSI/si_Tickets
        public ActionResult Index()
        {
            ViewBag.cmbEstado = objCombo._getCombo("ticket_estado");
            ViewBag.cmbPrioridad = objCombo._getCombo("ticket_prioridad");
            return View();
        }
        
        //MÉTODOS PARA LOS TICKETS
        #region TICKETS
        // JSON => Detalle de tickets
        [HttpPost]
        public JsonResult listarTicket(int ticketnumero = 0, string ticketempresa="",
                                        int ticketprioridad = 0,int ticketestado = 0,
                                        int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                List<TicketsModel> data = objTicket._getTickets(ticketnumero,ticketempresa,
                    ticketprioridad, ticketestado,jtStartIndex, jtPageSize);

                return Json(new { Result = "OK", Records = data, TotalRecordCount = objTicket.pro_getTotalREG }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar recuperar los tickets" });
            }
        }

        // GET: EDITAR
        public ActionResult editarTicket(string id)
        {
            if (id != null && id.Trim() != "")
            {
                Ticket ticketREG = objTicket._getTicketUUID(id);
                if (ticketREG != null)
                {
                    ViewBag.cmbPrioridadId = new SelectList(objCombo._getCombo("ticket_prioridad"), "Valor", "Descripcion", ticketREG.cmbPrioridadId);
                    ViewBag.cmbEstadoId = new SelectList(objCombo._getCombo("ticket_estado"), "Valor", "Descripcion", ticketREG.cmbPrioridadId);
                    ViewBag.TecnicoId = new SelectList(objTecnico._getTecnicos(), "id", "nombreTecnico", ticketREG.TecnicoId);
                    ViewBag.TicketCategoriaId = new SelectList(objCategoria._getCategorias(), "id", "Categoria", ticketREG.TicketCategoriaId);
                    return View(ticketREG);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: EDITAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editarTicket(Ticket ticket, string id)
        {
            try
            {
                objTicket._editTicket(ticket, id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }            
        }

        // GET: DETAILS
        public ActionResult verTicket(int id)
        {
            Ticket ticket = objTicket._getTicketUUID(id.ToString());
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        #endregion

        //MÉTODOS PARA LAS RESPUESTAS
        #region ANSWER

        // JSON => Detalle de respuestas
        public JsonResult listarAnswer(string id, int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                List<TicketsDETModel> data = objAnswer._getAnswers(id, jtStartIndex, jtPageSize);
                return Json(new { Result = "OK", Records = data.ToList(), TotalRecordCount = objAnswer.pro_getTotalREG }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar recuperar las respuestas" });
            }
        }

        // POST => Guarda respuesta
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult nuevoAnswer(string id,  string Mensaje, string observacion,string Minutos)
        {
            try
            {
                m_errors = "";
                if (_Validar(Minutos, Mensaje))
                {
                    List<TicketsDETModel> newAnswer = objAnswer._saveAnswer(id, Mensaje, observacion, Convert.ToInt32(Minutos));
                    if (newAnswer != null && newAnswer.Count == 1)
                    {
                        return Json(new { Result = "OK", Record = newAnswer[0] }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { Result = "ERROR", m_errors });
            }
            catch
            {
                return Json(new { Result = "ERROR", Message = "ERROR: Al intentar guardar la respuesta" });
            }
        }

        // GET => Detalle respuesta
        public ActionResult verAnswer(string id)
        {
            TicketsDetalle ticketsDetalle = objAnswer._getAnswerUUID(id);
            if (ticketsDetalle == null)
            {
                return HttpNotFound();
            }
            return View(ticketsDetalle);
        }

        #endregion


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region MétodosComplementarios

        private bool _Validar(string Minutos, string Mensaje)
        {
            bool isValid = true;
            if (Minutos.Trim() == "")
            {
                m_errors = "El campo 'Minutos' es obligatorio";
                isValid = false;
            }
            else if (Convert.ToInt32(Minutos) <= 0)
            {
                m_errors = "El campo 'Minutos' debe ser mayor a cero";
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
