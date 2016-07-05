using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using _Entidades.SentenciasSQL;
using _Helpers;

namespace _Entidades.specificModels
{
    public class TicketsModel
    {
        //private string m_originSI = "SUPPORT";
        private string m_originCLI = "CLIENT";
        private int m_totalREG = 0;
        
        //DEFINICIÓN DE LA CLASE
        #region Modelo
        public int id { get; set; }

        public DateTime Fecha { get; set; }
       
        public int Sec { get; set; }

        public string Categoria { get; set; }

        public string Usuario { get; set; }

        public string Tecnico { get; set; }

        public string Pregunta { get; set; }

        public string Prioridad { get; set; }

        public int Tiempo { get; set; }

        public int PrioridadID { get; set; }

        public int EstadoID { get; set; }

        public string Estado { get; set; }

        public string UUID { get; set; }

        public int newMSG { get; set; }

        //Solo Renán
        public string Empresa { get; set; }

        public string tipoPlan { get; set; }

        #endregion

        //MÉTODOS DEL SOPORTE
        #region Tickets-Support 
        public List<TicketsModel> _getTickets(int ticketnumero = 0, string ticketempresa = "",
                                        int ticketprioridad = 0, int ticketestado = 0,
                                        int jtStartIndex = 0, int jtPageSize = 0)
        {
            using (var dba = new _Context())
            {
                SqlParameter[] parametros = _Utilidades._ParamsSQL(
                                new string[] { "@startIndex", "@perPage", "@ticketNumero", "@ticketEmpresa", "@ticketPrioridad", "@ticketEstado","@TotalREG" },
                                new object[] { jtStartIndex, jtPageSize, ticketnumero, ticketempresa, ticketprioridad, ticketestado,0 },
                                new string[] { "@TotalREG" });
                DbRawSqlQuery<TicketsModel> data = dba.Database.SqlQuery<TicketsModel>
                                                            (_SQLSupport.RecuperaTickets, parametros);
                List<TicketsModel> listado = data.ToList();
                m_totalREG = Convert.ToInt32(parametros[6].Value);
                return listado;
            }
        }

        public Ticket _getTicketUUID(string UUID)
        {
            using (var dba = new _Context())
            {
                List<Ticket> lsTicket = dba.Ticket.Where(x => x.UUID == UUID).Take(1).ToList();
                if (lsTicket != null && lsTicket.Count == 1)
                {
                    return lsTicket[0];
                }
            }
            return null;
        }

        public void _editTicket(Ticket ticket, string id)
        {
            Ticket ticketREG = this._getTicketUUID(id);
            ticketREG.TecnicoId = ticket.TecnicoId;
            ticketREG.cmbEstadoId = ticket.cmbEstadoId;
            ticketREG.cmbPrioridadId = ticket.cmbPrioridadId;
            ticketREG.TicketCategoriaId = ticket.TicketCategoriaId;
            using (var dba = new _Context())
            {
                dba.Entry(ticketREG).State = EntityState.Modified;
                dba.SaveChanges();
            }
        }

        #endregion

        //MÉTODOS DE LOS CLIENTES
        #region Tickets-Client
        public List<TicketsModel> _getTicketsCLI(int ticketnumero = 0, int ticketestado = 0,
                                        int jtStartIndex = 0, int jtPageSize = 0)
        {
            using (var dba = new _Context())
            {
                int usuarioID = _SessionHelper.GetUserID();
                SqlParameter[] parametros = _Utilidades._ParamsSQL(
                    new string[] { "@UsuarioID", "@ticketNumero", "@ticketEstado", "@startIndex", "@perPage","@TotalREG" },
                    new object[] { usuarioID, ticketnumero, ticketestado, jtStartIndex, jtPageSize,0 },
                    new string[] {"@TotalREG" });
                DbRawSqlQuery<TicketsModel> data = dba.Database.SqlQuery<TicketsModel>
                                                            (_SQLCliente.RecuperaTickets, parametros);
                List<TicketsModel> listado = data.ToList();
                m_totalREG = Convert.ToInt32(parametros[5].Value);
                return listado;
            }
        }


        public List<TicketsModel> _saveTicketCLI(string teamViewer, string Pregunta)
        {
            using (var dba = new _Context())
            {
                int usuarioID = _SessionHelper.GetUserID();
                SqlParameter[] parametros = _Utilidades._ParamsSQL(
                            new string[] {"@UsuarioID","@TeamViewer","@Mensaje","@Observaciones",
                                    "@Archivo1","@Archivo2","@Archivo3","@whoSend" },
                            new object[] {usuarioID,teamViewer,Pregunta,
                                "","","","",m_originCLI});

                DbRawSqlQuery<TicketsModel> data = dba.Database.SqlQuery<TicketsModel>
                                                       (_SQLCliente.GuardaTickets, parametros);

                return data.ToList();
            }
        }

        #endregion

        //PROPIEDADES
        #region Propiedades

        //Total de registros
        #region proTotalREG
        public int pro_getTotalREG
        {
            get
            {
                return m_totalREG;
            }
        }
        #endregion

        #endregion

    }
}